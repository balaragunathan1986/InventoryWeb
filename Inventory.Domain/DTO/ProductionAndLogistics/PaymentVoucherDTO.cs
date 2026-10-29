using Inventory.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    [Table("Payment_Voucher")]
    public class PaymentVoucherDTO : AuditFields
    {
        [Key]
        public int Id { get; set; }
        public Guid Payment_Voucher_Id { get; set; }
        public string Payment_Voucher_Number { get; set; } = string.Empty;
        public DateTime Payment_Voucher_Date { get; set; }
        public int Company_Id { get; set; }
        public int? Supplier_Id { get; set; }
        [NotMapped]
        public string? Supplier_Name { get; set; }
        public Guid? PaymentMode_Id { get; set; }
        [NotMapped]
        public string? PaymentMode { get; set; }
        public Guid? PaymentTerms_Id { get; set; }
        [NotMapped]
        public string? PaymentTerms { get; set; }
        public string? Reference_Number { get; set; }
        public decimal Total_Amount { get; set; }
        public string Status { get; set; } = "Draft";
        public string? Remarks { get; set; }
    }
}
