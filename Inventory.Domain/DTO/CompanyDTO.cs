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
    [Table("Company")]
    public class CompanyDTO : AuditFields
    {
        [Key]
        public int Id { get; set; }
        public Guid Company_Id { get; set; }
        public string? Company_Name { get; set; }
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Company_State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string? Phone_Number { get; set; }
        public string? Email { get; set; }
        public string Gst_Number { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }



    }
}
