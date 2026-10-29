using Inventory.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    [Table("Billing_Receipt_Details")]
    public class BillingReceiptDetailDTO : AuditFields
    {
        [Key]
        public int Id { get; set; }
        public Guid Billing_Receipt_Detail_Id { get; set; }
        public Guid Billing_Receipt_Id { get; set; }
        public string Deduction_Type { get; set; } = string.Empty;
        public decimal Deduction_Quantity { get; set; }
        public decimal? Deduction_Percentage { get; set; }
    }
}