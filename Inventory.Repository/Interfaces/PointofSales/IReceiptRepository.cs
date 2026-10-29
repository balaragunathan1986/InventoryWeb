using Inventory.Domain.DTO.PointofSales;

namespace Inventory.Repository.Interfaces.PointofSales;

public interface IReceiptRepository
{
    Task<string> GenerateNumber();
    Task<List<ReceiptDTO>> GetReceipts();
    Task<ReceiptDTO> GetReceipt(Guid receiptId);
    Task<ReceiptDTO> Save(ReceiptSaveRequest receipt);
    Task<ReceiptDTO> Update(ReceiptSaveRequest receipt);
    Task<bool> Delete(Guid receiptId);
}