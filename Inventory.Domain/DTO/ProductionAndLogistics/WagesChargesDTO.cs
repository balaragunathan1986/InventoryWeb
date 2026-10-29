using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    [Table("Wages_Charges")]
    public class WagesChargesDTO 
    {
        [Key]
        public int Id { get; set; }
        public Guid Wages_Charge_Id { get; set; }
        public Guid Wages_Id { get; set; }
        public Guid Charge_Id { get; set; }
        [Column(TypeName = "decimal(18,3)")]
        public decimal Percentage { get; set; }
        [Column(TypeName = "decimal(18,3)")]
        public decimal Amount { get; set; }
    }
}
