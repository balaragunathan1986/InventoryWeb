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
    [Table("Unit_Of_Measurement")]
    public class UomDTO : AuditFields
    {
        [Key]
        public int Id { get; set; }
        public Guid Uom_Id { get; set; }
        public  string? Uom {  get; set; }
        public  string? Abbrevation { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
