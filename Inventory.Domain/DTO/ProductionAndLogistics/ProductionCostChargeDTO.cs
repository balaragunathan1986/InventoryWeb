using Inventory.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain.DTO.ProductionAndLogistics
{

    [Table("Production_Cost_Charges")]
    public class ProductionCostChargeDTO : AuditFields
    {
        [Key]
        public int Id { get; set; }
        public Guid Production_Cost_Charges_Id { get; set; }
        public Guid Production_Cost_Id { get; set; }
        public Guid Charge_Id { get; set; }
        public decimal Percentage { get; set; }
        public decimal Amount { get; set; }
        public string? Remarks { get; set; }
    }



}
