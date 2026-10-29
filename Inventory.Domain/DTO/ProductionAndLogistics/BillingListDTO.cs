namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    public class BillingListDTO
    {
        public int Id { get; set; }
        public Guid Billing_Id { get; set; }
        public string Billing_Number { get; set; } = string.Empty;
        public DateTime? Billing_Date { get; set; }
        public int Supplier_Id { get; set; }
        public string Supplier_Name { get; set; } = string.Empty;
        public decimal Total_Bill_Qty { get; set; }
        public decimal Total_Bill_Amount { get; set; }
        public int Payment_Status { get; set; }
    }
}