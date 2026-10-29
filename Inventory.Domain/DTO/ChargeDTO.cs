using Inventory.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.DTO
{
    [Table("Charge")]
    public class ChargeDTO : AuditFields
    {
        [Key]
        public int Id { get; set; }
        public Guid Charge_Id { get; set; }
        public string? ChargeName { get; set; }
        public string? ChargeCode { get; set; }
        public string? ChargeType { get; set; }
        public decimal ChargePercentage { get; set; }
        public string? ChargeDescription { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
