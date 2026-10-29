using Inventory.Domain.DTO.ProductionAndLogistics;

namespace Inventory.Domain.Model.Production
{
    public class ProductionBatchModel : AuditFields
    {  

        public int Id { get; set; }
        public Guid Production_Batch_Id { get; set; }
        public string Production_Batch_Number { get; set; } = string.Empty;
        public int Company_Id { get; set; }
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public string? Remarks { get; set; }
        public int Supervisor_Id { get; set; }
        public int Production_Status { get; set; }
        public List<ProductionBatchDetailDTO> Production_Batch_Details { get; set; } = new();
    }
}
 