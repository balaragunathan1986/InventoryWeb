using Inventory.Domain.Model;

namespace Inventory.Domain.Model.Production
{
    public class DespatchModel : AuditFields
    {
        public int Id { get; set; }
        public Guid Production_Despatch_Id { get; set; }
        public string Production_Despatch_Number { get; set; } = string.Empty;
        public DateTime? Despatch_Date { get; set; }
        public Guid Production_Batch_Id { get; set; }
        public int Company_Id { get; set; }
        public decimal Batch_Qty { get; set; }
        public decimal First_Weight { get; set; }
        public decimal Second_Weight { get; set; }
        public decimal Despatch_Qty { get; set; }
        public decimal Loss_Qty { get; set; }
        public decimal Loss_Percentage { get; set; }
        public string? Remarks { get; set; }
    }
}