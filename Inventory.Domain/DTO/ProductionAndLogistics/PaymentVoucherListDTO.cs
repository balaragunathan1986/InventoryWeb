namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    public class PaymentVoucherListDTO
    {
        public int Id { get; set; }
        public Guid Payment_Voucher_Id { get; set; }
        public string Payment_Voucher_Number { get; set; } = string.Empty;
        public DateTime Payment_Voucher_Date { get; set; }
        public int? Supplier_Id { get; set; }
        public string? Supplier_Name { get; set; }

        public string? Reference_Number { get; set; }
        public decimal Total_Amount { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
