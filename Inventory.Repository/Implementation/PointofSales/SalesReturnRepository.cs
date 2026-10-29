using Inventory.Domain.DTO.PointofSales;
using Inventory.Domain.DTO.Inventory;
using Inventory.Repository.Data;
using Inventory.Repository.Interfaces.PointofSales;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Repository.Implementation.PointofSales;

public class SalesReturnRepository : ISalesReturnRepository
{
    private readonly AppDBContext _dbContext;

    public SalesReturnRepository(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> GenerateNumber()
    {
        var lastNumber = await _dbContext.SalesReturns
            .OrderByDescending(item => item.Sales_Return_Number)
            .Select(item => item.Sales_Return_Number)
            .FirstOrDefaultAsync();

        return Utility.CommonFunctions.GenerateDocumentNumber(
            Utility.Costants.SalesReturn,
            Utility.CommonFunctions.GetTransactionNumber(lastNumber));
    }

    public async Task<List<SalesReturnDTO>> GetSalesReturns()
    {
        var returns = await _dbContext.SalesReturns
            .OrderByDescending(item => item.Sales_Return_Date)
            .ToListAsync();

        foreach (var item in returns)
        {
            await LoadDetailsAsync(item);
        }

        return returns;
    }

    public async Task<SalesReturnDTO> GetSalesReturn(Guid salesReturnId)
    {
        var item = await _dbContext.SalesReturns
            .FirstOrDefaultAsync(value => value.Sales_Return_Id == salesReturnId);

        if (item is null)
        {
            return new SalesReturnDTO();
        }

        await LoadDetailsAsync(item);
        return item;
    }

    public async Task<SalesDTO> GetSalesForReturn(Guid salesId)
    {
        var sale = await _dbContext.Sales.FirstOrDefaultAsync(item => item.Sales_Id == salesId);
        if (sale is null)
        {
            return new SalesDTO();
        }

        var details = await (
            from detail in _dbContext.SalesDetails
            join product in _dbContext.Product on detail.Product_Id equals product.Product_Id into products
            from product in products.DefaultIfEmpty()
            join uom in _dbContext.Uom on detail.UOM_Id equals uom.Uom_Id into uoms
            from uom in uoms.DefaultIfEmpty()
            where detail.Sales_Id == salesId
            select new SalesDetailDTO
            {
                Id = detail.Id,
                Sales_Detail_Id = detail.Sales_Detail_Id,
                Sales_Id = detail.Sales_Id,
                Product_Id = detail.Product_Id,
                Product_Name = product == null ? null : product.ProductName,
                UOM_Id = detail.UOM_Id,
                UOM_Name = uom == null ? null : uom.Uom,
                Quantity = detail.Quantity,
                Rate = detail.Rate,
                Gross_Amount = detail.Gross_Amount,
                Remarks = detail.Remarks,
                CreatedBy = detail.CreatedBy,
                CreatedAt = detail.CreatedAt,
                UpdatedBy = detail.UpdatedBy,
                UpdatedAt = detail.UpdatedAt
            }).ToListAsync();

        await SetAvailableQuantitiesAsync(details);
        sale.Customer_Name = await _dbContext.Customer
            .Where(item => item.Id == sale.Customer_Id)
            .Select(item => item.Customer_Name)
            .FirstOrDefaultAsync();
        sale.Details = details;
        return sale;
    }

    public async Task<SalesReturnDTO> Save(SalesReturnSaveRequest request)
    {
        ValidateRequest(request);
        await ValidateQuantitiesAsync(request.Sales_Id, request.Sales_Return_Details, null);
        await ValidateFullReturnAsync(request);

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        var entity = ToEntity(request);
        await _dbContext.SalesReturns.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        await AddDetailsAsync(request, entity);
        await _dbContext.SaveChangesAsync();
        await AddReturnedStockAsync(request, entity);
        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();

        await LoadDetailsAsync(entity);
        return entity;
    }

    public async Task<SalesReturnDTO> Update(SalesReturnSaveRequest request)
    {
        ValidateRequest(request);
        var entity = await _dbContext.SalesReturns
            .FirstOrDefaultAsync(item => item.Sales_Return_Id == request.Sales_Return_Id && item.Id == request.Id);

        if (entity is null)
        {
            return new SalesReturnDTO();
        }

        await ValidateQuantitiesAsync(request.Sales_Id, request.Sales_Return_Details, request.Sales_Return_Id);
        await ValidateFullReturnAsync(request, request.Sales_Return_Id);
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        entity.Sales_Return_Number = request.Sales_Return_Number;
        entity.Sales_Return_Date = request.Sales_Return_Date;
        entity.Sales_Id = request.Sales_Id;
        entity.Customer_Id = request.Customer_Id;
        entity.Return_Type = request.Return_Type;
        entity.Company_Id = request.Company_Id;
        entity.Sub_Total = request.Sales_Return_Details.Sum(item => item.Quantity * item.Rate);
        entity.Charge_Total = 0;
        entity.Net_Amount = entity.Sub_Total;
        entity.Remarks = request.Remarks;
        entity.UpdatedBy = request.UpdatedBy;
        entity.UpdatedAt = request.UpdatedAt ?? DateTime.UtcNow;

        var details = await _dbContext.SalesReturnDetails
            .Where(item => item.Sales_Return_Id == entity.Sales_Return_Id).ToListAsync();
        var returnStocks = await _dbContext.SalesReturnStocks
            .Where(item => details.Select(detail => detail.Sales_Return_Detail_Id).Contains(item.Sales_Return_Detail_Id))
            .ToListAsync();
        var productStockIds = returnStocks
            .Where(item => item.Stock_Id.HasValue)
            .Select(item => item.Stock_Id!.Value)
            .ToList();
        var productStocks = await _dbContext.ProductStock
            .Where(item => productStockIds.Contains(item.ProductStock_Id))
            .ToListAsync();
        _dbContext.ProductStock.RemoveRange(productStocks);
        _dbContext.SalesReturnStocks.RemoveRange(returnStocks);
        await _dbContext.SaveChangesAsync();

        _dbContext.SalesReturnDetails.RemoveRange(details);
        await _dbContext.SaveChangesAsync();
        await AddDetailsAsync(request, entity);
        await _dbContext.SaveChangesAsync();
        await AddReturnedStockAsync(request, entity);
        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();

        await LoadDetailsAsync(entity);
        return entity;
    }

    public async Task<bool> Delete(Guid salesReturnId, string? deletedBy)
    {
        var entity = await _dbContext.SalesReturns
            .FirstOrDefaultAsync(item => item.Sales_Return_Id == salesReturnId);
        if (entity is null)
        {
            return false;
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        var details = await _dbContext.SalesReturnDetails
            .Where(item => item.Sales_Return_Id == salesReturnId).ToListAsync();
        var returnStocks = await _dbContext.SalesReturnStocks
            .Where(item => details.Select(detail => detail.Sales_Return_Detail_Id).Contains(item.Sales_Return_Detail_Id))
            .ToListAsync();
        var productStockIds = returnStocks
            .Where(item => item.Stock_Id.HasValue)
            .Select(item => item.Stock_Id!.Value)
            .ToList();
        var productStocks = await _dbContext.ProductStock
            .Where(item => productStockIds.Contains(item.ProductStock_Id))
            .ToListAsync();
        _dbContext.ProductStock.RemoveRange(productStocks);
        _dbContext.SalesReturnStocks.RemoveRange(returnStocks);
        await _dbContext.SaveChangesAsync();

        _dbContext.SalesReturnDetails.RemoveRange(details);
        _dbContext.SalesReturns.Remove(entity);
        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
        return true;
    }

    private async Task LoadDetailsAsync(SalesReturnDTO entity)
    {
        entity.Details = await (
            from detail in _dbContext.SalesReturnDetails
            join product in _dbContext.Product on detail.Product_Id equals product.Product_Id into products
            from product in products.DefaultIfEmpty()
            join uom in _dbContext.Uom on detail.UOM_Id equals uom.Uom_Id into uoms
            from uom in uoms.DefaultIfEmpty()
            where detail.Sales_Return_Id == entity.Sales_Return_Id
            select new SalesReturnDetailDTO
            {
                Id = detail.Id,
                Sales_Return_Detail_Id = detail.Sales_Return_Detail_Id,
                Sales_Return_Id = detail.Sales_Return_Id,
                Sales_Detail_Id = detail.Sales_Detail_Id,
                Product_Id = detail.Product_Id,
                Product_Name = product == null ? null : product.ProductName,
                UOM_Id = detail.UOM_Id,
                UOM_Name = uom == null ? null : uom.Uom,
                Quantity = detail.Quantity,
                Rate = detail.Rate,
                Gross_Amount = detail.Gross_Amount,
                Return_Reason = detail.Return_Reason,
                Remarks = detail.Remarks,
                CreatedBy = detail.CreatedBy,
                CreatedAt = detail.CreatedAt,
                UpdatedBy = detail.UpdatedBy,
                UpdatedAt = detail.UpdatedAt
            }).ToListAsync();

        var stocks = await _dbContext.SalesReturnStocks
            .Where(item => entity.Details.Select(detail => detail.Sales_Return_Detail_Id)
                .Contains(item.Sales_Return_Detail_Id))
            .ToListAsync();
        foreach (var detail in entity.Details)
        {
            detail.Stocks = stocks
                .Where(stock => stock.Sales_Return_Detail_Id == detail.Sales_Return_Detail_Id)
                .ToList();
        }

        await SetAvailableQuantitiesAsync(entity.Details, entity.Sales_Return_Id);
        entity.Sales_Number = await _dbContext.Sales
            .Where(item => item.Sales_Id == entity.Sales_Id)
            .Select(item => item.Sales_Number).FirstOrDefaultAsync();
        entity.Customer_Name = await _dbContext.Customer
            .Where(item => item.Id == entity.Customer_Id)
            .Select(item => item.Customer_Name)
            .FirstOrDefaultAsync();
    }

    private async Task SetAvailableQuantitiesAsync(List<SalesDetailDTO> details)
    {
        foreach (var detail in details)
        {
            detail.Available_Quantity = await GetAvailableQuantityAsync(detail.Sales_Detail_Id, null);
        }
    }

    private async Task SetAvailableQuantitiesAsync(List<SalesReturnDetailDTO> details, Guid? currentReturnId = null)
    {
        foreach (var detail in details)
        {
            var sourceQuantity = await _dbContext.SalesDetails
                .Where(item => item.Sales_Detail_Id == detail.Sales_Detail_Id)
                .Select(item => item.Quantity).FirstOrDefaultAsync();
            var returnedQuantity = await _dbContext.SalesReturnDetails
                .Where(item => item.Sales_Detail_Id == detail.Sales_Detail_Id && item.Sales_Return_Id != currentReturnId)
                .SumAsync(item => (decimal?)item.Quantity) ?? 0;
            detail.Available_Quantity = sourceQuantity - returnedQuantity;
        }
    }

    private async Task<decimal> GetAvailableQuantityAsync(Guid salesDetailId, Guid? ignoredReturnId)
    {
        var sourceQuantity = await _dbContext.SalesDetails
            .Where(item => item.Sales_Detail_Id == salesDetailId)
            .Select(item => item.Quantity).FirstOrDefaultAsync();
        var returnedQuantity = await _dbContext.SalesReturnDetails
            .Where(item => item.Sales_Detail_Id == salesDetailId && item.Sales_Return_Id != ignoredReturnId)
            .SumAsync(item => (decimal?)item.Quantity) ?? 0;
        return sourceQuantity - returnedQuantity;
    }

    private async Task ValidateQuantitiesAsync(Guid salesId, List<SalesReturnDetailSaveRequest> details, Guid? ignoredReturnId)
    {
        var sourceDetailIds = await _dbContext.SalesDetails
            .Where(item => item.Sales_Id == salesId)
            .Select(item => item.Sales_Detail_Id).ToListAsync();

        if (details.Any(item => !sourceDetailIds.Contains(item.Sales_Detail_Id)))
        {
            throw new InvalidOperationException("Every return line must belong to the selected sales entry.");
        }

        if (details.GroupBy(item => item.Sales_Detail_Id).Any(group => group.Count() > 1))
        {
            throw new InvalidOperationException("A sales detail can only be returned once in a return document.");
        }

        foreach (var detail in details)
        {
            var available = await GetAvailableQuantityAsync(detail.Sales_Detail_Id, ignoredReturnId);
            if (detail.Quantity <= 0 || detail.Quantity > available)
            {
                throw new InvalidOperationException(
                    $"Return quantity for detail '{detail.Sales_Detail_Id}' must be greater than zero and no more than {available:0.###}.");
            }
        }
    }

    private async Task ValidateFullReturnAsync(SalesReturnSaveRequest request, Guid? ignoredReturnId = null)
    {
        if (!string.Equals(request.Return_Type, nameof(SalesReturnType.Full), StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        var sourceDetails = await _dbContext.SalesDetails
            .Where(item => item.Sales_Id == request.Sales_Id)
            .Select(item => new { item.Sales_Detail_Id, item.Quantity })
            .ToListAsync();
        var requested = request.Sales_Return_Details.ToDictionary(item => item.Sales_Detail_Id, item => item.Quantity);

        if (sourceDetails.Count != requested.Count)
        {
            throw new InvalidOperationException("A full return must return the available quantity for every sales detail.");
        }

        foreach (var source in sourceDetails)
        {
            var available = await GetAvailableQuantityAsync(source.Sales_Detail_Id, ignoredReturnId);
            if (!requested.TryGetValue(source.Sales_Detail_Id, out var quantity) || quantity != available)
            {
                throw new InvalidOperationException("A full return must return the available quantity for every sales detail.");
            }
        }
    }

    private static void ValidateRequest(SalesReturnSaveRequest request)
    {
        if (request.Sales_Id == Guid.Empty || request.Sales_Return_Details.Count == 0)
        {
            throw new InvalidOperationException("A sales entry and at least one return detail are required.");
        }

        if (string.Equals(request.Return_Type, nameof(SalesReturnType.Full), StringComparison.OrdinalIgnoreCase))
        {
            request.Return_Type = nameof(SalesReturnType.Full);
        }
        else
        {
            request.Return_Type = nameof(SalesReturnType.Partial);
        }
    }

    private static SalesReturnDTO ToEntity(SalesReturnSaveRequest request) => new()
    {
        Sales_Return_Id = request.Sales_Return_Id == Guid.Empty ? Guid.NewGuid() : request.Sales_Return_Id,
        Sales_Return_Number = request.Sales_Return_Number,
        Sales_Return_Date = request.Sales_Return_Date ?? DateTime.UtcNow,
        Sales_Id = request.Sales_Id,
        Customer_Id = request.Customer_Id,
        Return_Type = request.Return_Type,
        Company_Id = request.Company_Id,
        Sub_Total = request.Sales_Return_Details.Sum(item => item.Quantity * item.Rate),
        Charge_Total = 0,
        Net_Amount = request.Sales_Return_Details.Sum(item => item.Quantity * item.Rate),
        Remarks = request.Remarks,
        CreatedBy = request.CreatedBy,
        CreatedAt = request.CreatedAt ?? DateTime.UtcNow
    };

    private async Task AddDetailsAsync(SalesReturnSaveRequest request, SalesReturnDTO entity)
    {
        var details = request.Sales_Return_Details.Select(item => new SalesReturnDetailDTO
        {
            Sales_Return_Detail_Id = item.Sales_Return_Detail_Id == Guid.Empty ? Guid.NewGuid() : item.Sales_Return_Detail_Id,
            Sales_Return_Id = entity.Sales_Return_Id,
            Sales_Detail_Id = item.Sales_Detail_Id,
            Product_Id = item.Product_Id,
            UOM_Id = item.UOM_Id,
            Quantity = item.Quantity,
            Rate = item.Rate,
            Gross_Amount = item.Quantity * item.Rate,
            Return_Reason = item.Return_Reason,
            Remarks = item.Remarks,
            CreatedBy = item.CreatedBy ?? request.CreatedBy,
            CreatedAt = item.CreatedAt ?? request.CreatedAt ?? DateTime.UtcNow,
            UpdatedBy = item.UpdatedBy,
            UpdatedAt = item.UpdatedAt
        });

        await _dbContext.SalesReturnDetails.AddRangeAsync(details);
    }

    private async Task AddReturnedStockAsync(SalesReturnSaveRequest request, SalesReturnDTO entity)
    {
        foreach (var stockRequest in request.Sales_Return_Stocks)
        {
            var detail = await _dbContext.SalesReturnDetails.FirstOrDefaultAsync(item =>
                item.Sales_Return_Id == entity.Sales_Return_Id &&
                item.Sales_Return_Detail_Id == stockRequest.Sales_Return_Detail_Id);

            if (detail is null)
            {
                throw new InvalidOperationException("Returned stock must reference a saved return detail.");
            }

            if (stockRequest.Returned_Qty <= 0 || stockRequest.Returned_Qty > detail.Quantity)
            {
                throw new InvalidOperationException("Returned stock quantity must be greater than zero and no more than the return quantity.");
            }

            var productStockNo = string.IsNullOrWhiteSpace(stockRequest.ProductStockNo)
                ? await GenerateProductStockNumberAsync()
                : stockRequest.ProductStockNo.Trim();

            if (await _dbContext.ProductStock.AnyAsync(item =>
                    item.ProductStockNo == productStockNo && !item.IsDeleted))
            {
                throw new InvalidOperationException($"Product stock number '{productStockNo}' already exists.");
            }

            var productStock = new ProductStockDTO
            {
                ProductStock_Id = Guid.NewGuid(),
                ProductStockNo = productStockNo,
                Qty = stockRequest.Returned_Qty,
                Rate = detail.Rate,
                AllotedQty = 0,
                Product_Id = detail.Product_Id,
                SQty = 0,
                BalQty = stockRequest.Returned_Qty,
                ProductStockDate = entity.Sales_Return_Date,
                Company_Id = entity.Company_Id,
                ReturnQty = stockRequest.Returned_Qty,
                Uom_Id = detail.UOM_Id,
                Remarks = $"Sales Return {entity.Sales_Return_Number} - {stockRequest.Stock_Condition ?? "Saleable"}",
                MarkupRate = 0,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = stockRequest.CreatedBy ?? request.CreatedBy,
                CreatedAt = stockRequest.CreatedAt ?? request.CreatedAt ?? DateTime.UtcNow
            };

            await _dbContext.ProductStock.AddAsync(productStock);
            await _dbContext.SaveChangesAsync();

            await _dbContext.SalesReturnStocks.AddAsync(new SalesReturnStockDTO
            {
                Sales_Return_Stock_Id = stockRequest.Sales_Return_Stock_Id == Guid.Empty
                    ? Guid.NewGuid() : stockRequest.Sales_Return_Stock_Id,
                Sales_Return_Detail_Id = detail.Sales_Return_Detail_Id,
                Stock_Id = productStock.ProductStock_Id,
                Batch_Id = stockRequest.Batch_Id,
                Returned_Qty = stockRequest.Returned_Qty,
                Stock_Condition = stockRequest.Stock_Condition,
                Remarks = stockRequest.Remarks,
                CreatedBy = stockRequest.CreatedBy ?? request.CreatedBy,
                CreatedAt = stockRequest.CreatedAt ?? request.CreatedAt ?? DateTime.UtcNow
            });
        }
    }

    private async Task<string> GenerateProductStockNumberAsync()
    {
        var lastNumber = await _dbContext.ProductStock
            .OrderByDescending(item => item.ProductStockNo)
            .Select(item => item.ProductStockNo)
            .FirstOrDefaultAsync();

        return Utility.CommonFunctions.GenerateDocumentNumber(
            Utility.Costants.ProductStock,
            Utility.CommonFunctions.GetTransactionNumber(lastNumber));
    }
}