using Inventory.Domain.DTO.ProductionAndLogistics;
using Inventory.Domain.Model;

namespace Inventory.Domain.Model.Production
{
    public class BillingModel : AuditFields
    {
        public int Id { get; set; }
        public Guid Billing_Id { get; set; }
        public string Billing_Number { get; set; } = string.Empty;
        public DateTime? Billing_Date { get; set; }
        public int Supplier_Id { get; set; }
        public int Company_Id { get; set; }
        public decimal Total_Bill_Qty { get; set; }
        public decimal Total_Bill_Amount { get; set; }
        public int Payment_Status { get; set; }
        public string? Remarks { get; set; }
        public List<BillingDetailDTO> Billing_Details { get; set; } = new();
    }
}