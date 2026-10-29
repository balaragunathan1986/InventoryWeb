namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    public class JobReceiptListDTO
    {
        public Guid Job_Receipt_Id { get; set; }
        public string? Job_Receipt_Number { get; set; }
        public DateTime? Job_Receipt_Date { get; set; }
        public string Supplier_Name { get; set; } = string.Empty;
        public decimal? Total_Qty { get; set; }
        public decimal? Total_Amount { get; set; }
        public bool? Is_Selected { get; set; }


        
    }
}