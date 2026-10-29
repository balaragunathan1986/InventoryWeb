using Inventory.Domain.DTO.PointofSales;
using Inventory.Repository.Interfaces.PointofSales;
using Inventory.Services.Interfaces.PointofSales;

namespace Inventory.Services.Implementation.PointofSales;

public class ReceiptService : IReceiptService
{
    private readonly IReceiptRepository _receiptRepository;

    public ReceiptService(IReceiptRepository receiptRepository)
    {
        _receiptRepository = receiptRepository;
    }

    public Task<string> GenerateDocumentNumber() => _receiptRepository.GenerateNumber();

    public Task<List<ReceiptDTO>> GetReceipts() => _receiptRepository.GetReceipts();

    public Task<ReceiptDTO> GetReceipt(Guid receiptId) => _receiptRepository.GetReceipt(receiptId);

    public Task<ReceiptDTO> Save(ReceiptSaveRequest receipt) => _receiptRepository.Save(receipt);

    public Task<ReceiptDTO> Update(ReceiptSaveRequest receipt) => _receiptRepository.Update(receipt);

    public Task<bool> Delete(Guid receiptId) => _receiptRepository.Delete(receiptId);
}