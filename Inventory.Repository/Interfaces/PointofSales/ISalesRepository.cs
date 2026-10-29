using Inventory.Domain.DTO.PointofSales;

namespace Inventory.Repository.Interfaces.PointofSales;

public interface ISalesRepository
{
    Task<string> GenerateNumber();
    Task<List<SalesDTO>> GetSales();
    Task<SalesDTO> GetSales(Guid salesId);
    Task<SalesDTO> Save(SalesSaveRequest sales);
    Task<SalesDTO> Update(SalesSaveRequest sales);
    Task<bool> Delete(Guid salesId);
}
