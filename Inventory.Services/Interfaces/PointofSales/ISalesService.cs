using Inventory.Domain.DTO.PointofSales;

namespace Inventory.Services.Interfaces.PointofSales;

public interface ISalesService
{
    Task<string> GenerateDocumentNumber();
    Task<List<SalesDTO>> GetSales();
    Task<SalesDTO> GetSales(Guid salesId);
    Task<SalesDTO> Save(SalesSaveRequest sales);
    Task<SalesDTO> Update(SalesSaveRequest sales);
    Task<bool> Delete(Guid salesId);
}
