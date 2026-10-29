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
    [Table("Sales_Type")]
    public class SalesTypeDTO : AuditFields
    {
        [Key]
        public int Id { get; set; }
        public Guid SalesType_Id { get; set; }
        public string? SalesType { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
