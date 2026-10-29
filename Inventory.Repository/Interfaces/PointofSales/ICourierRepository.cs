using Inventory.Domain.DTO.PointofSales;

namespace Inventory.Repository.Interfaces.PointofSales;

public interface ICourierRepository
{
    Task<List<CourierDTO>> GetCouriers();
    Task<CourierDTO> GetCourier(Guid courierId);
    Task<CourierDTO> Save(CourierSaveRequest request);
    Task<CourierDTO> Update(CourierSaveRequest request);
    Task<bool> Delete(Guid courierId);
}
