using Inventory.Domain.DTO.PointofSales;
using Inventory.Repository.Interfaces.PointofSales;
using Inventory.Services.Interfaces.PointofSales;

namespace Inventory.Services.Implementation.PointofSales;

public class SalesReturnService : ISalesReturnService
{
    private readonly ISalesReturnRepository _repository;

    public SalesReturnService(ISalesReturnRepository repository)
    {
        _repository = repository;
    }

    public Task<string> GenerateDocumentNumber() => _repository.GenerateNumber();
    public Task<List<SalesReturnDTO>> GetSalesReturns() => _repository.GetSalesReturns();
    public Task<SalesReturnDTO> GetSalesReturn(Guid salesReturnId) => _repository.GetSalesReturn(salesReturnId);
    public Task<SalesDTO> GetSalesForReturn(Guid salesId) => _repository.GetSalesForReturn(salesId);
    public Task<SalesReturnDTO> Save(SalesReturnSaveRequest request) => _repository.Save(request);
    public Task<SalesReturnDTO> Update(SalesReturnSaveRequest request) => _repository.Update(request);
    public Task<bool> Delete(Guid salesReturnId, string? deletedBy) => _repository.Delete(salesReturnId, deletedBy);
}