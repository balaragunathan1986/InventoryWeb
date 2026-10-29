using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    [Table("Job_Receipt_Charge")]
    public class JobReceiptChargeDTO
    {
        [Key]
        public int Id { get; set; }
        public Guid Job_Receipt_Charge_Id { get; set; }
        public Guid Job_Receipt_Id { get; set; }
        public Guid Charge_Id { get; set; }
        [Column(TypeName = "decimal(18,3)")]
        public decimal Percentage { get; set; }
        [Column(TypeName = "decimal(18,3)")]
        public decimal Amount { get; set; }
    }
}

