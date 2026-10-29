using Inventory.Domain.DTO.PointofSales;
using Inventory.Repository.Data;
using Inventory.Repository.Interfaces.PointofSales;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Repository.Implementation.PointofSales;

public class ReceiptRepository : IReceiptRepository
{
    private readonly AppDBContext _dbContext;

    public ReceiptRepository(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> GenerateNumber()
    {
        var lastNumber = await _dbContext.Receipts
            .OrderByDescending(receipt => receipt.Receipt_Number)
            .Select(receipt => receipt.Receipt_Number)
            .FirstOrDefaultAsync();

        return Utility.CommonFunctions.GenerateDocumentNumber(
            Utility.Costants.Receipt,
            Utility.CommonFunctions.GetTransactionNumber(lastNumber));
    }

    public async Task<List<ReceiptDTO>> GetReceipts()
    {
        var receipts = await _dbContext.Receipts
            .Where(receipt => receipt.DeletedAt == null)
            .OrderByDescending(receipt => receipt.Receipt_Date)
            .ToListAsync();

        foreach (var receipt in receipts)
        {
            await LoadAllocationsAsync(receipt);
        }

        return receipts;
    }

    public async Task<ReceiptDTO> GetReceipt(Guid receiptId)
    {
        var receipt = await _dbContext.Receipts
            .FirstOrDefaultAsync(item => item.Receipt_Id == receiptId && item.DeletedAt == null);

        if (receipt is null)
        {
            return new ReceiptDTO();
        }

        await LoadAllocationsAsync(receipt);
        return receipt;
    }

    public async Task<ReceiptDTO> Save(ReceiptSaveRequest request)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        var receipt = ToReceipt(request);
        await _dbContext.Receipts.AddAsync(receipt);
        await _dbContext.SaveChangesAsync();
        await AddAllocationsAsync(request, receipt);
        await UpdateSalesPaymentStatusAsync(
            request.Receipt_Allocations.Select(allocation => allocation.Sales_Id),
            request.CreatedBy,
            request.CreatedAt ?? DateTime.UtcNow);
        await transaction.CommitAsync();

        await LoadAllocationsAsync(receipt);
        return receipt;
    }

    public async Task<ReceiptDTO> Update(ReceiptSaveRequest request)
    {
        var receipt = await _dbContext.Receipts.FirstOrDefaultAsync(item =>
            item.Receipt_Id == request.Receipt_Id && item.Id == request.Id && item.DeletedAt == null);

        if (receipt is null)
        {
            return new ReceiptDTO();
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        receipt.Receipt_Number = request.Receipt_Number;
        receipt.Receipt_Date = request.Receipt_Date;
        receipt.Customer_Id = request.Customer_Id;
        receipt.Company_Id = request.Company_Id;
        receipt.PaymentMode_Id = request.PaymentMode_Id;
        receipt.Reference_Number = request.Reference_Number;
        receipt.Amount = request.Amount;
        receipt.Remarks = request.Remarks;
        receipt.UpdatedBy = request.UpdatedBy;
        receipt.UpdatedAt = request.UpdatedAt ?? DateTime.UtcNow;

        var existingAllocations = await _dbContext.ReceiptAllocations
            .Where(allocation => allocation.Receipt_Id == receipt.Receipt_Id)
            .ToListAsync();
        var activeAllocations = existingAllocations
            .Where(allocation => allocation.DeletedAt == null)
            .ToList();

        var affectedSalesIds = activeAllocations.Select(allocation => allocation.Sales_Id)
            .Concat(request.Receipt_Allocations.Select(allocation => allocation.Sales_Id));
        var retainedAllocations = new HashSet<ReceiptAllocationDTO>();
        var allocationsToAdd = new List<ReceiptAllocationDTO>();

        foreach (var allocationRequest in request.Receipt_Allocations)
        {
            var allocation = allocationRequest.Receipt_Allocation_Id == Guid.Empty
                ? null
                : existingAllocations.FirstOrDefault(existing =>
                    existing.Receipt_Allocation_Id == allocationRequest.Receipt_Allocation_Id);

            if (allocation is null)
            {
                allocationsToAdd.Add(new ReceiptAllocationDTO
                {
                    Receipt_Allocation_Id = allocationRequest.Receipt_Allocation_Id == Guid.Empty
                        ? Guid.NewGuid()
                        : allocationRequest.Receipt_Allocation_Id,
                    Receipt_Id = receipt.Receipt_Id,
                    Sales_Id = allocationRequest.Sales_Id,
                    Allocated_Amount = allocationRequest.Allocated_Amount,
                    CreatedBy = allocationRequest.CreatedBy ?? request.CreatedBy,
                    CreatedAt = allocationRequest.CreatedAt ?? request.CreatedAt ?? DateTime.UtcNow
                });
                continue;
            }

            allocation.Sales_Id = allocationRequest.Sales_Id;
            allocation.Allocated_Amount = allocationRequest.Allocated_Amount;
            allocation.DeletedBy = null;
            allocation.DeletedAt = null;
            allocation.UpdatedBy = request.UpdatedBy;
            allocation.UpdatedAt = receipt.UpdatedAt;
            retainedAllocations.Add(allocation);
        }

        foreach (var allocation in activeAllocations)
        {
            if (!retainedAllocations.Contains(allocation))
            {
                allocation.DeletedBy = request.UpdatedBy;
                allocation.DeletedAt = receipt.UpdatedAt;
            }
        }

        await _dbContext.ReceiptAllocations.AddRangeAsync(allocationsToAdd);
        await _dbContext.SaveChangesAsync();
        await UpdateSalesPaymentStatusAsync(
            affectedSalesIds,
            request.UpdatedBy,
            receipt.UpdatedAt.Value);
        await transaction.CommitAsync();

        await LoadAllocationsAsync(receipt);
        return receipt;
    }

    public async Task<bool> Delete(Guid receiptId)
    {
        var receipt = await _dbContext.Receipts
            .FirstOrDefaultAsync(item => item.Receipt_Id == receiptId && item.DeletedAt == null);

        if (receipt is null)
        {
            return false;
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        var deletedAt = DateTime.UtcNow;
        receipt.DeletedBy = "system";
        receipt.DeletedAt = deletedAt;

        var allocations = await _dbContext.ReceiptAllocations
            .Where(allocation => allocation.Receipt_Id == receiptId && allocation.DeletedAt == null)
            .ToListAsync();
        foreach (var allocation in allocations)
        {
            allocation.DeletedBy = "system";
            allocation.DeletedAt = deletedAt;
        }

        await _dbContext.SaveChangesAsync();
        await UpdateSalesPaymentStatusAsync(
            allocations.Select(allocation => allocation.Sales_Id),
            "system",
            deletedAt);
        await transaction.CommitAsync();
        return true;
    }

    private async Task LoadAllocationsAsync(ReceiptDTO receipt)
    {
        receipt.Customer_Name = await _dbContext.Customer
            .Where(customer => customer.Id == receipt.Customer_Id)
            .Select(customer => customer.Customer_Name)
            .FirstOrDefaultAsync();

        receipt.PaymentMode_Name = await _dbContext.PaymentMode
            .Where(mode => mode.PaymentMode_Id == receipt.PaymentMode_Id)
            .Select(mode => mode.PaymentMode)
            .FirstOrDefaultAsync();

        receipt.Allocations = await (
            from allocation in _dbContext.ReceiptAllocations
            join sales in _dbContext.Sales on allocation.Sales_Id equals sales.Sales_Id into salesRecords
            from sales in salesRecords.DefaultIfEmpty()
            where allocation.Receipt_Id == receipt.Receipt_Id && allocation.DeletedAt == null
            select new ReceiptAllocationDTO
            {
                Id = allocation.Id,
                Receipt_Allocation_Id = allocation.Receipt_Allocation_Id,
                Receipt_Id = allocation.Receipt_Id,
                Sales_Id = allocation.Sales_Id,
                Sales_Number = sales == null ? null : sales.Sales_Number,
                Allocated_Amount = allocation.Allocated_Amount,
                CreatedBy = allocation.CreatedBy,
                CreatedAt = allocation.CreatedAt,
                UpdatedBy = allocation.UpdatedBy,
                UpdatedAt = allocation.UpdatedAt,
                DeletedBy = allocation.DeletedBy,
                DeletedAt = allocation.DeletedAt
            }).ToListAsync();
    }

    private async Task AddAllocationsAsync(ReceiptSaveRequest request, ReceiptDTO receipt)
    {
        var allocations = request.Receipt_Allocations.Select(allocation => new ReceiptAllocationDTO
        {
            Receipt_Allocation_Id = allocation.Receipt_Allocation_Id == Guid.Empty
                ? Guid.NewGuid()
                : allocation.Receipt_Allocation_Id,
            Receipt_Id = receipt.Receipt_Id,
            Sales_Id = allocation.Sales_Id,
            Allocated_Amount = allocation.Allocated_Amount,
            CreatedBy = allocation.CreatedBy ?? request.CreatedBy,
            CreatedAt = allocation.CreatedAt ?? request.CreatedAt ?? DateTime.UtcNow,
            UpdatedBy = allocation.UpdatedBy,
            UpdatedAt = allocation.UpdatedAt
        }).ToList();

        await _dbContext.ReceiptAllocations.AddRangeAsync(allocations);
        await _dbContext.SaveChangesAsync();
    }

    private async Task UpdateSalesPaymentStatusAsync(
        IEnumerable<Guid> salesIds,
        string? updatedBy,
        DateTime updatedAt)
    {
        var affectedSalesIds = salesIds.Distinct().ToList();
        if (affectedSalesIds.Count == 0)
        {
            return;
        }

        var sales = await _dbContext.Sales
            .Where(sale => affectedSalesIds.Contains(sale.Sales_Id))
            .ToListAsync();
        var paidAmounts = await _dbContext.ReceiptAllocations
            .Where(allocation => affectedSalesIds.Contains(allocation.Sales_Id) && allocation.DeletedAt == null)
            .GroupBy(allocation => allocation.Sales_Id)
            .Select(group => new { SalesId = group.Key, Amount = group.Sum(allocation => allocation.Allocated_Amount) })
            .ToDictionaryAsync(item => item.SalesId, item => item.Amount);

        foreach (var sale in sales)
        {
            sale.Paid_Amount = paidAmounts.GetValueOrDefault(sale.Sales_Id);
            sale.Balance_Amount = Math.Max(sale.Net_Amount - sale.Paid_Amount, 0);
            sale.Payment_Status = sale.Paid_Amount <= 0
                ? "Unpaid"
                : sale.Balance_Amount <= 0
                    ? "Paid"
                    : "Partially Paid";
            sale.UpdatedBy = updatedBy;
            sale.UpdatedAt = updatedAt;
        }

        await _dbContext.SaveChangesAsync();
    }

    private static ReceiptDTO ToReceipt(ReceiptSaveRequest request)
    {
        return new ReceiptDTO
        {
            Receipt_Id = request.Receipt_Id == Guid.Empty ? Guid.NewGuid() : request.Receipt_Id,
            Receipt_Number = request.Receipt_Number,
            Receipt_Date = request.Receipt_Date,
            Customer_Id = request.Customer_Id,
            Company_Id = request.Company_Id,
            PaymentMode_Id = request.PaymentMode_Id,
            Reference_Number = request.Reference_Number,
            Amount = request.Amount,
            Remarks = request.Remarks,
            CreatedBy = request.CreatedBy,
            CreatedAt = request.CreatedAt ?? DateTime.UtcNow,
            UpdatedBy = request.UpdatedBy,
            UpdatedAt = request.UpdatedAt
        };
    }
}