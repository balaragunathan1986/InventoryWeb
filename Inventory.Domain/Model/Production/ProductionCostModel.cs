using Inventory.Domain.DTO.ProductionAndLogistics;
using Inventory.Domain.Model;

namespace Inventory.Domain.Model.Production
{
    public class ProductionCostModel : AuditFields
    {
        public int Id { get; set; }
        public Guid Production_Cost_Id { get; set; }
        public string Production_Cost_Number { get; set; } = string.Empty;
        public Guid Production_Batch_Id { get; set; }
        public int Company_Id { get; set; }
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public DateTime? ScreeningStart_Date { get; set; }
        public DateTime? ScreeningEnd_Date { get; set; }
        public decimal Total_Expenses { get; set; }
        public string? Remarks { get; set; }
        public List<ProductionCostChargeDTO> Production_Cost_Charges { get; set; } = new();
        public List<ProductionCostScreeningDTO> Production_Cost_Screenings { get; set; } = new();
    }
}
