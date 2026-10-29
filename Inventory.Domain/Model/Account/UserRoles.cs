using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Model
{
    [Table("UserRoles")]
    public class UserRoles
    {
        public Guid user_Role_Id {  get; set; }
        public Guid user_Id { get; set; }   
        public Guid role_Id { get; set; }
    }
}
