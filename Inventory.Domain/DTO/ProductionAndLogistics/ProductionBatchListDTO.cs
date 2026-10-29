namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    public class ProductionBatchListDTO
    {
        public int Id { get; set; }
        public Guid Production_Batch_Id { get; set; }
        public string Production_Batch_Number { get; set; } = string.Empty;
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public decimal Total_Input_Qty { get; set; }
        public List<JobReceiptListDTO> OpenJobReceipts { get; set; } = new();
    }
}
