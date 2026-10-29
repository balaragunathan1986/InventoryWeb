using Inventory.Domain.DTO.PointofSales;
using Inventory.Repository.Interfaces.PointofSales;
using Inventory.Services.Interfaces.PointofSales;

namespace Inventory.Services.Implementation.PointofSales;

public class CourierService : ICourierService
{
    private readonly ICourierRepository _courierRepository;

    public CourierService(ICourierRepository courierRepository)
    {
        _courierRepository = courierRepository;
    }

    public Task<List<CourierDTO>> GetCouriers() => _courierRepository.GetCouriers();

    public Task<CourierDTO> GetCourier(Guid courierId) => _courierRepository.GetCourier(courierId);

    public Task<CourierDTO> Save(CourierSaveRequest request) => _courierRepository.Save(request);

    public Task<CourierDTO> Update(CourierSaveRequest request) => _courierRepository.Update(request);

    public Task<bool> Delete(Guid courierId) => _courierRepository.Delete(courierId);
}
