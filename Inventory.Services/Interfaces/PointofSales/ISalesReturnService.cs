using Inventory.Domain.DTO.PointofSales;

namespace Inventory.Services.Interfaces.PointofSales;

public interface ISalesReturnService
{
    Task<string> GenerateDocumentNumber();
    Task<List<SalesReturnDTO>> GetSalesReturns();
    Task<SalesReturnDTO> GetSalesReturn(Guid salesReturnId);
    Task<SalesDTO> GetSalesForReturn(Guid salesId);
    Task<SalesReturnDTO> Save(SalesReturnSaveRequest request);
    Task<SalesReturnDTO> Update(SalesReturnSaveRequest request);
    Task<bool> Delete(Guid salesReturnId, string? deletedBy);
}