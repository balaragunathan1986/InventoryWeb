namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    public class DespatchListDTO
    {
        public int Id { get; set; }
        public Guid Production_Despatch_Id { get; set; }
        public string Production_Despatch_Number { get; set; } = string.Empty;
        public DateTime? Despatch_Date { get; set; }
        public Guid Production_Batch_Id { get; set; }
        public string Production_Batch_Number { get; set; } = string.Empty;
        public decimal? Batch_Qty { get; set; }
        public decimal? Despatch_Qty { get; set; }
        public decimal? Loss_Qty { get; set; }
        public decimal? Loss_Percentage { get; set; }
    }
}