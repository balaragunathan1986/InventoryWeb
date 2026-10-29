using Inventory.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    [Table("Billing_Master")]
    public class BillingMasterDTO : AuditFields
    {
        [Key]
        public int Id { get; set; }
        public Guid Billing_Id { get; set; }
        public string Billing_Number { get; set; } = string.Empty;
        public DateTime? Billing_Date { get; set; }
        public int Supplier_Id { get; set; }
        [NotMapped]
        public string Supplier_Name { get; set; } = string.Empty;
        public int Company_Id { get; set; }
        public decimal Total_Bill_Qty { get; set; }
        public decimal Total_Bill_Amount { get; set; }
        public int Payment_Status { get; set; }
        public string? Remarks { get; set; }
        [NotMapped]
        public List<BillingDetailDTO> Billing_Details { get; set; } = new();
    }
}