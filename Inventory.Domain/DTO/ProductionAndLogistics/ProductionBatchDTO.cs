using Inventory.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    [Table("Production_Batch_Master")]
    public class ProductionBatchDTO : AuditFields
    {
        [Key]
        public int Id { get; set; }
        public Guid Production_Batch_Id { get; set; }
        public string Production_Batch_Number { get; set; } = string.Empty;
        public int Company_Id { get; set; }
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public int Supervisor_Id { get; set; }
        public int Production_Status { get; set; }
        public string? Remarks { get; set; }     
    }
}
