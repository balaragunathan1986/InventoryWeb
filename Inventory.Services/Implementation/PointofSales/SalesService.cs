using Inventory.Domain.DTO.PointofSales;
using Inventory.Repository.Interfaces.PointofSales;
using Inventory.Services.Interfaces.PointofSales;

namespace Inventory.Services.Implementation.PointofSales;

public class SalesService : ISalesService
{
    private readonly ISalesRepository _salesRepository;

    public SalesService(ISalesRepository salesRepository)
    {
        _salesRepository = salesRepository;
    }

    public Task<string> GenerateDocumentNumber() => _salesRepository.GenerateNumber();

    public Task<List<SalesDTO>> GetSales() => _salesRepository.GetSales();

    public Task<SalesDTO> GetSales(Guid salesId) => _salesRepository.GetSales(salesId);

    public Task<SalesDTO> Save(SalesSaveRequest sales) => _salesRepository.Save(sales);

    public Task<SalesDTO> Update(SalesSaveRequest sales) => _salesRepository.Update(sales);

    public Task<bool> Delete(Guid salesId) => _salesRepository.Delete(salesId);
}
