using Inventory.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    [Table("Production_Cost_Screening")]
    public class ProductionCostScreeningDTO : AuditFields
    {
        [Key]
        public int Id { get; set; }
        public Guid Production_Cost_Screening_Id { get; set; }
        public Guid Production_Cost_Id { get; set; }
        public string Days { get; set; } = string.Empty;
        public int Persons { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public string? Remarks { get; set; }
    }
}
