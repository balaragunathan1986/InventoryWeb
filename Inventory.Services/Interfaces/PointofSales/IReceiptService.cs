using Inventory.Domain.DTO.PointofSales;

namespace Inventory.Services.Interfaces.PointofSales;

public interface IReceiptService
{
    Task<string> GenerateDocumentNumber();
    Task<List<ReceiptDTO>> GetReceipts();
    Task<ReceiptDTO> GetReceipt(Guid receiptId);
    Task<ReceiptDTO> Save(ReceiptSaveRequest receipt);
    Task<ReceiptDTO> Update(ReceiptSaveRequest receipt);
    Task<bool> Delete(Guid receiptId);
}