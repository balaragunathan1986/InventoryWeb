using Inventory.Domain.Model;

namespace Inventory.Domain.Model.Production
{
    public class PaymentVoucherModel : AuditFields
    {
        public int Id { get; set; }
        public Guid Payment_Voucher_Id { get; set; }
        public string Payment_Voucher_Number { get; set; } = string.Empty;
        public DateTime Payment_Voucher_Date { get; set; }
        public int Company_Id { get; set; }
        public int? Supplier_Id { get; set; }
        public Guid? PaymentMode_Id { get; set; }
        public Guid? PaymentTerms_Id { get; set; }
        public string? Reference_Number { get; set; }
        public decimal Total_Amount { get; set; }
        public string Status { get; set; } = "Draft";
        public string? Remarks { get; set; }
    }
}
