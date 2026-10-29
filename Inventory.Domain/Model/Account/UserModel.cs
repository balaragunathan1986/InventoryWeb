using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Model.Account
{
    public class UserModel
    {
        public required string User_Name { get; set; }
        public required string Password { get; set; }
    }
}
