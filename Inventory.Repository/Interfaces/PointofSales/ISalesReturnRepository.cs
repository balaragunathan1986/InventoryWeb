using Inventory.Domain.DTO.PointofSales;

namespace Inventory.Repository.Interfaces.PointofSales;

public interface ISalesReturnRepository
{
    Task<string> GenerateNumber();
    Task<List<SalesReturnDTO>> GetSalesReturns();
    Task<SalesReturnDTO> GetSalesReturn(Guid salesReturnId);
    Task<SalesDTO> GetSalesForReturn(Guid salesId);
    Task<SalesReturnDTO> Save(SalesReturnSaveRequest request);
    Task<SalesReturnDTO> Update(SalesReturnSaveRequest request);
    Task<bool> Delete(Guid salesReturnId, string? deletedBy);
}