using Inventory.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    [Table("Production_Batch_Details")]
    public class ProductionBatchDetailDTO : AuditFields
    {
        [Key]
        public int Id { get; set; }
        public Guid Production_Batch_Detail_Id { get; set; }
        public Guid Production_Batch_Id { get; set; }
        public Guid Job_Receipt_Master_Id { get; set; }
        public decimal Input_Quantity { get; set; }
        public string? Remarks { get; set; }
        public bool? Is_Selected { get; set; }
    }
}