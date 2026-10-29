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
    [Table("Product_Group")]
    public class ProductGroupDTO : AuditFields
    {
        [Key]
        public int Id { get; set; }
        public Guid ProductGroup_Id { get; set; }
        public string? ProductGroupName { get; set; }
        public string? ProductGroupCode { get; set; }
        public string? ProductGroupDescription { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
