using Inventory.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    [Table("Production_Cost_Master")]
    public class ProductionCostDTO:AuditFields
    {
        [Key]
        public int Id { get; set; }
        public Guid Production_Cost_Id { get; set; }
        public string Production_Cost_Number { get; set; } = string.Empty;
        [NotMapped]
        public string Production_Batch_Number { get; set; } = string.Empty;
        public Guid Production_Batch_Id { get; set; }
        public int Company_Id { get; set; }
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public DateTime? ScreeningStart_Date { get; set; }
        public DateTime? ScreeningEnd_Date { get; set; }
        public decimal Total_Expenses { get; set; }
        public string? Remarks { get; set; }
        [NotMapped]
        public List<ProductionCostChargeDTO> Production_Cost_Charges { get; set; } = new();
        [NotMapped]
        public List<ProductionCostScreeningDTO> Production_Cost_Screenings { get; set; } = new();
    }
}
