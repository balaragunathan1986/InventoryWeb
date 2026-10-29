using Inventory.Domain.DTO.PointofSales;
using Inventory.Repository.Data;
using Inventory.Repository.Interfaces.PointofSales;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Repository.Implementation.PointofSales;

public class CourierRepository : ICourierRepository
{
    private readonly AppDBContext _dbContext;

    public CourierRepository(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CourierDTO>> GetCouriers()
    {
        var couriers = await _dbContext.Courier
            //.Where(courier => !courier.IsDeleted)
            .OrderByDescending(courier => courier.Dispatch_Date)
            .ThenByDescending(courier => courier.Id)
            .ToListAsync();

        await LoadSalesNumbersAsync(couriers);
        return couriers;
    }

    public async Task<CourierDTO> GetCourier(Guid courierId)
    {
        var courier = await _dbContext.Courier
            .FirstOrDefaultAsync(item => item.Courier_Id == courierId);

        if (courier is null)
        {
            return new CourierDTO();
        }

        await LoadSalesNumbersAsync(new List<CourierDTO> { courier });
        return courier;
    }

    public async Task<CourierDTO> Save(CourierSaveRequest request)
    {
        var courier = ToCourier(request);
        await _dbContext.Courier.AddAsync(courier);
        await _dbContext.SaveChangesAsync();
        await LoadSalesNumbersAsync(new List<CourierDTO> { courier });
        return courier;
    }

    public async Task<CourierDTO> Update(CourierSaveRequest request)
    {
        var courier = await _dbContext.Courier
            .FirstOrDefaultAsync(item => item.Courier_Id == request.Courier_Id
                && item.Id == request.Id);
               

        if (courier is null)
        {
            return new CourierDTO();
        }

        courier.Sales_Id = request.Sales_Id;
        courier.AWB = request.AWB;
        courier.Courier_Company = request.Courier_Company;
        courier.Sender = request.Sender;
        courier.Receiver = request.Receiver;
        courier.Origin = request.Origin;
        courier.Destination = request.Destination;
        courier.Weight = request.Weight;
        courier.Dimensions = request.Dimensions;
        courier.Charges = request.Charges;
        courier.Dispatch_Date = request.Dispatch_Date;
        courier.Expected_Delivery_Date = request.Expected_Delivery_Date;
        courier.Courier_Status = request.Courier_Status;
        courier.Remarks = request.Remarks;   
        courier.UpdatedBy = request.UpdatedBy;
        courier.UpdatedAt = request.UpdatedAt;

        await _dbContext.SaveChangesAsync();
        await LoadSalesNumbersAsync(new List<CourierDTO> { courier });
        return courier;
    }

    public async Task<bool> Delete(Guid courierId)
    {
        var courier = await _dbContext.Courier
            .FirstOrDefaultAsync(item => item.Courier_Id == courierId);

        if (courier is null)
        {
            return false;
        }

        _dbContext.Courier.Remove(courier);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    private static CourierDTO ToCourier(CourierSaveRequest request)
    {
        return new CourierDTO
        {
            Courier_Id = request.Courier_Id == Guid.Empty ? Guid.NewGuid() : request.Courier_Id,
            Sales_Id = request.Sales_Id,
            AWB = request.AWB,
            Courier_Company = request.Courier_Company,
            Sender = request.Sender,
            Receiver = request.Receiver,
            Origin = request.Origin,
            Destination = request.Destination,
            Weight = request.Weight,
            Dimensions = request.Dimensions,
            Charges = request.Charges,
            Dispatch_Date = request.Dispatch_Date,
            Expected_Delivery_Date = request.Expected_Delivery_Date,
            Courier_Status = string.IsNullOrWhiteSpace(request.Courier_Status) ? "In Transit" : request.Courier_Status,
            Remarks = request.Remarks,
            CreatedBy = request.CreatedBy,
            CreatedAt = request.CreatedAt,
            //IsActive = request.IsActive,
            //IsDeleted = request.IsDeleted
        };
    }

    private async Task LoadSalesNumbersAsync(List<CourierDTO> couriers)
    {
        var salesIds = couriers.Select(courier => courier.Sales_Id).Distinct().ToList();
        var salesNumbers = await _dbContext.Sales
            .Where(sale => salesIds.Contains(sale.Sales_Id))
            .ToDictionaryAsync(sale => sale.Sales_Id, sale => sale.Sales_Number);

        foreach (var courier in couriers)
        {
            courier.Sales_Number = salesNumbers.GetValueOrDefault(courier.Sales_Id);
        }
    }
}
