using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Model.Account
{
    [Table("AspNetRoles")]
    public class Roles
    {
        [Key]
        public Guid role_Id {  get; set; }
        public required string role_Name { get; set; }
    }
}
