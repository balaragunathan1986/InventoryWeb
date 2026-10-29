using Inventory.Domain.DTO.PointofSales;
using Inventory.Repository.Data;
using Inventory.Repository.Interfaces.PointofSales;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Repository.Implementation.PointofSales;

public class SalesRepository : ISalesRepository
{
    private readonly AppDBContext _dbContext;

    public SalesRepository(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> GenerateNumber()
    {
        var lastNumber = await _dbContext.Sales
            .OrderByDescending(sale => sale.Sales_Number)
            .Select(sale => sale.Sales_Number)
            .FirstOrDefaultAsync();

        return Utility.CommonFunctions.GenerateDocumentNumber(
            Utility.Costants.Sales,
            Utility.CommonFunctions.GetTransactionNumber(lastNumber));
    }

    public async Task<List<SalesDTO>> GetSales()
    {
        var sales = await _dbContext.Sales
        
            .OrderByDescending(sales => sales.Sales_Date)
            .ToListAsync();

        foreach (var sale in sales)
        {
            await LoadDetailsAsync(sale);
        }

        return sales;
    }

    public async Task<SalesDTO> GetSales(Guid salesId)
    {
        var sale = await _dbContext.Sales
            .FirstOrDefaultAsync(sales => sales.Sales_Id == salesId);

        if (sale is null)
        {
            return new SalesDTO();
        }

        await LoadDetailsAsync(sale);
        return sale;
    }

    public async Task<SalesDTO> Save(SalesSaveRequest request)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        var sale = ToSales(request);
        await _dbContext.Sales.AddAsync(sale);
        await _dbContext.SaveChangesAsync();
        await AddChildrenAsync(request, sale);
        await UpdateProductStockAsync(GetRequestedAllocations(request), 1);
        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();

        await LoadDetailsAsync(sale);
        return sale;
    }

    public async Task<SalesDTO> Update(SalesSaveRequest request)
    {
        var sale = await _dbContext.Sales
            .FirstOrDefaultAsync(item => item.Sales_Id == request.Sales_Id && item.Id == request.Id);

        if (sale is null)
        {
            return new SalesDTO();
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        sale.Sales_Number = request.Sales_Number;
        sale.Sales_Date = request.Sales_Date;
        sale.Customer_Id = request.Customer_Id;
        sale.Sales_Type = request.Sales_Type;
        sale.Company_Id = request.Company_Id;
       // sale.PaymentMode_Id = request.PaymentMode_Id;
        sale.PaymentTerms_Id = request.PaymentTerms_Id;
        sale.Sub_Total = request.Sub_Total;
        sale.Charge_Total = request.Charge_Total;
        sale.Net_Amount = request.Net_Amount;
        sale.Balance_Amount = Math.Max(sale.Net_Amount - sale.Paid_Amount, 0);
        sale.Payment_Status = GetPaymentStatus(sale.Paid_Amount, sale.Balance_Amount);
        sale.Remarks = request.Remarks;
        sale.Terms_Conditions = request.Terms_Conditions;
        //sale.IsActive = request.IsActive;
        sale.UpdatedBy = request.UpdatedBy;
        sale.UpdatedAt = request.UpdatedAt;

        var details = await _dbContext.SalesDetails.Where(item => item.Sales_Id == sale.Sales_Id).ToListAsync();
        var charges = await _dbContext.SalesCharges.Where(item => item.Sales_Id == sale.Sales_Id).ToListAsync();
        var detailIds = details.Select(item => item.Sales_Detail_Id).ToList();
        var allocations = await _dbContext.SalesStockAllocations
            .Where(item => detailIds.Contains(item.Sales_Detail_Id))
            .ToListAsync();
        _dbContext.SalesStockAllocations.RemoveRange(allocations);
        await UpdateProductStockAsync(allocations, -1);
        await _dbContext.SaveChangesAsync();
        _dbContext.SalesDetails.RemoveRange(details);
        _dbContext.SalesCharges.RemoveRange(charges);
        await _dbContext.SaveChangesAsync();
        await AddChildrenAsync(request, sale);
        await UpdateProductStockAsync(GetRequestedAllocations(request), 1);
        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();

        await LoadDetailsAsync(sale);
        return sale;
    }

    public async Task<bool> Delete(Guid salesId)
    {
        var sale = await _dbContext.Sales
            .FirstOrDefaultAsync(item => item.Sales_Id == salesId);

        if (sale is null)
        {
            return false;
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        //sale.IsDeleted = true;
        //sale.IsActive = false;
        sale.DeletedBy = "system";
        sale.DeletedAt = DateTime.UtcNow;

        var details = await _dbContext.SalesDetails.Where(item => item.Sales_Id == salesId).ToListAsync();
        var charges = await _dbContext.SalesCharges.Where(item => item.Sales_Id == salesId).ToListAsync();
        var detailIds = details.Select(item => item.Sales_Detail_Id).ToList();
        var allocations = await _dbContext.SalesStockAllocations
            .Where(item => detailIds.Contains(item.Sales_Detail_Id))
            .ToListAsync();
        _dbContext.SalesStockAllocations.RemoveRange(allocations);
        await UpdateProductStockAsync(allocations, -1);
        await _dbContext.SaveChangesAsync();
        _dbContext.SalesDetails.RemoveRange(details);
        _dbContext.SalesCharges.RemoveRange(charges);
        _dbContext.Sales.RemoveRange(sale);
        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
        return true;
    }

    private async Task LoadDetailsAsync(SalesDTO sale)
    {
        //if (sale.PaymentMode_Id.HasValue)
        //{
        //    sale.PaymentMode_Name = await _dbContext.PaymentMode
        //        .Where(mode => mode.PaymentMode_Id == sale.PaymentMode_Id)
        //        .Select(mode => mode.PaymentMode)
        //        .FirstOrDefaultAsync();
        //}

        if (sale.PaymentTerms_Id.HasValue)
        {
            sale.PaymentTerms_Name = await _dbContext.PaymentTerms
                .Where(terms => terms.PaymentTerms_Id == sale.PaymentTerms_Id)
                .Select(terms => terms.PaymentTerms)
                .FirstOrDefaultAsync();
        }

        sale.Details = await (
            from detail in _dbContext.SalesDetails
            join product in _dbContext.Product on detail.Product_Id equals product.Product_Id into products
            from product in products.DefaultIfEmpty()
            join uom in _dbContext.Uom on detail.UOM_Id equals uom.Uom_Id into uoms
            from uom in uoms.DefaultIfEmpty()
            where detail.Sales_Id == sale.Sales_Id
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

        var allocations = await _dbContext.SalesStockAllocations
            .Where(allocation => sale.Details.Select(detail => detail.Sales_Detail_Id).Contains(allocation.Sales_Detail_Id))
            .ToListAsync();

        foreach (var detail in sale.Details)
        {
            detail.Stock_Allocations = allocations
                .Where(allocation => allocation.Sales_Detail_Id == detail.Sales_Detail_Id)
                .ToList();
        }

        sale.Charges = await (
            from charge in _dbContext.SalesCharges
            join master in _dbContext.Charge on charge.Charge_Id equals master.Charge_Id into masters
            from master in masters.DefaultIfEmpty()
            where charge.Sales_Id == sale.Sales_Id
            select new SalesChargeDTO
            {
                Id = charge.Id,
                Sales_Charge_Id = charge.Sales_Charge_Id,
                Sales_Id = charge.Sales_Id,
                Charge_Id = charge.Charge_Id,
                Charge_Name = master == null ? null : master.ChargeName,
                Charge_Type = charge.Charge_Type,
                Charge_Percentage = charge.Charge_Percentage,
                Charge_Amount = charge.Charge_Amount,
                Remarks = charge.Remarks,
                CreatedBy = charge.CreatedBy,
                CreatedAt = charge.CreatedAt,
                UpdatedBy = charge.UpdatedBy,
                UpdatedAt = charge.UpdatedAt
            }).ToListAsync();
    }

    private async Task AddChildrenAsync(SalesSaveRequest request, SalesDTO sale)
    {
        var details = request.Sales_Details.Select(detail => new SalesDetailDTO
        {
            Sales_Detail_Id = detail.Sales_Detail_Id == Guid.Empty ? Guid.NewGuid() : detail.Sales_Detail_Id,
            Sales_Id = sale.Sales_Id,
            Product_Id = detail.Product_Id,
            UOM_Id = detail.UOM_Id,
            Quantity = detail.Quantity,
            Rate = detail.Rate,
            Gross_Amount = detail.Gross_Amount,
            Remarks = detail.Remarks,
            CreatedBy = detail.CreatedBy ?? request.CreatedBy,
            CreatedAt = detail.CreatedAt ?? request.CreatedAt,
            UpdatedBy = detail.UpdatedBy,
            UpdatedAt = detail.UpdatedAt
        }).ToList();

        var charges = request.Sales_Charges.Select(charge => new SalesChargeDTO
        {
            Sales_Charge_Id = charge.Sales_Charge_Id == Guid.Empty ? Guid.NewGuid() : charge.Sales_Charge_Id,
            Sales_Id = sale.Sales_Id,
            Charge_Id = charge.Charge_Id,
            Charge_Type = charge.Charge_Type,
            Charge_Percentage = charge.Charge_Percentage,
            Charge_Amount = charge.Charge_Amount,
            Remarks = charge.Remarks,
            CreatedBy = charge.CreatedBy ?? request.CreatedBy,
            CreatedAt = charge.CreatedAt ?? request.CreatedAt,
            UpdatedBy = charge.UpdatedBy,
            UpdatedAt = charge.UpdatedAt
        });

        await _dbContext.SalesDetails.AddRangeAsync(details);
        await _dbContext.SalesCharges.AddRangeAsync(charges);
        var allocations = request.Sales_Details
            .SelectMany((detail, index) => detail.Stock_Allocations.Select(allocation => new SalesStockAllocationDTO
            {
                Sales_Stock_Allocation_Id = allocation.Sales_Stock_Allocation_Id == Guid.Empty
                    ? Guid.NewGuid()
                    : allocation.Sales_Stock_Allocation_Id,
                Sales_Detail_Id = details[index].Sales_Detail_Id,
                Stock_Id = allocation.Stock_Id,
                Batch_Id = allocation.Batch_Id,
                Allocated_Qty = allocation.Allocated_Qty,
                CreatedBy = allocation.CreatedBy ?? request.CreatedBy,
                CreatedAt = allocation.CreatedAt ?? request.CreatedAt,
                UpdatedBy = allocation.UpdatedBy,
                UpdatedAt = allocation.UpdatedAt
            }))
            .ToList();

        await _dbContext.SalesStockAllocations.AddRangeAsync(allocations);
        await _dbContext.SaveChangesAsync();
    }

    private static IEnumerable<SalesStockAllocationDTO> GetRequestedAllocations(SalesSaveRequest request)
    {
        return request.Sales_Details.SelectMany(detail => detail.Stock_Allocations);
    }

    private async Task UpdateProductStockAsync(
        IEnumerable<SalesStockAllocationDTO> allocations,
        decimal direction)
    {
        var quantitiesByStock = allocations
            .GroupBy(allocation => allocation.Stock_Id)
            .Select(group => new
            {
                StockId = group.Key,
                Quantity = group.Sum(allocation => allocation.Allocated_Qty) * direction
            })
            .Where(item => item.Quantity != 0)
            .ToList();

        if (quantitiesByStock.Count == 0)
        {
            return;
        }

        var stockIds = quantitiesByStock.Select(item => item.StockId).ToList();
        var stocks = await _dbContext.ProductStock
            .Where(stock => stockIds.Contains(stock.ProductStock_Id) && !stock.IsDeleted)
            .ToListAsync();

        var stockById = stocks.ToDictionary(stock => stock.ProductStock_Id);
        foreach (var item in quantitiesByStock)
        {
            if (!stockById.TryGetValue(item.StockId, out var stock))
            {
                throw new InvalidOperationException($"Product stock '{item.StockId}' was not found.");
            }

            var allotedQty = (stock.AllotedQty ?? 0) + item.Quantity;
            if (allotedQty < 0)
            {
                throw new InvalidOperationException($"Product stock '{item.StockId}' has an invalid allocated quantity.");
            }

            stock.AllotedQty = allotedQty;
            stock.BalQty = stock.Qty - allotedQty;
        }
    }

    private static SalesDTO ToSales(SalesSaveRequest request)
    {
        var balance = Math.Max(request.Net_Amount, 0);
        return new SalesDTO
        {
            Sales_Id = request.Sales_Id == Guid.Empty ? Guid.NewGuid() : request.Sales_Id,
            Sales_Number = request.Sales_Number,
            Sales_Date = request.Sales_Date,
            Customer_Id = request.Customer_Id,
            Sales_Type = request.Sales_Type,
            Company_Id = request.Company_Id,
            //PaymentMode_Id = request.PaymentMode_Id,
            PaymentTerms_Id = request.PaymentTerms_Id,
            Sub_Total = request.Sub_Total,
            Charge_Total = request.Charge_Total,
            Net_Amount = request.Net_Amount,
            Paid_Amount = 0,
            Balance_Amount = balance,
            Payment_Status = GetPaymentStatus(0, balance),
            Remarks = request.Remarks,
            Terms_Conditions = request.Terms_Conditions,
            //IsActive = request.IsActive,
            //IsDeleted = false,
            CreatedBy = request.CreatedBy,
            CreatedAt = request.CreatedAt ?? DateTime.UtcNow,
            UpdatedBy = request.UpdatedBy,
            UpdatedAt = request.UpdatedAt
        };
    }

    private static string GetPaymentStatus(decimal paidAmount, decimal balance)
    {
        return paidAmount <= 0 ? "Unpaid" : balance <= 0 ? "Paid" : "Partially Paid";
    }
}
