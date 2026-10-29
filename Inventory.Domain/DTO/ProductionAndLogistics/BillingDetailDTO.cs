using Inventory.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    [Table("Billing_Details")]
    public class BillingDetailDTO : AuditFields
    {
        [Key]
        public int Id { get; set; }
        public Guid Billing_Detail_Id { get; set; }
        public Guid Billing_Id { get; set; }
        public Guid Production_Despatch_Id { get; set; }
        public string Production_Despatch_Number { get; set; } = string.Empty;
        public DateTime? Despatch_Date { get; set; }
        public decimal Net_Bill_Qty { get; set; }
        public decimal Rate_Per_Kg { get; set; }
        public decimal Bill_Amount { get; set; }
    }
}