using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Model.Account
{
    [Table("Email_Setup", Schema = "dbo")]
    public class EmailSetup
    {
        [Key]
        public Guid? email_Setup_Id { get; set; }
        public string? Smtp_Host { get; set; }
        public  string? Smtp_Port { get; set; }
        public  string? email { get; set; }
        public  string? password { get; set; }
        public bool? isDefault { get; set; }
        public bool? isActive { get; set; }
        public bool? isDeleted { get; set; }

    }
}
