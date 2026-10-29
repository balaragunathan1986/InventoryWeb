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
    [Table("Wages")]
    public class WagesDTO : AuditFields
    {
        [Key]
        public int Id { get; set; }
        public required Guid Wages_Id { get; set; }
        public required string Wages_Number { get; set; }
        public DateTime Wages_Date { get; set; }
        public string? Wages_Remarks { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Wages_Amount { get; set; }
        public bool isDeleted { get; set; }
    }
}
