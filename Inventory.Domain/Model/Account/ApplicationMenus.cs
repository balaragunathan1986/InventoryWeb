using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Model.Account
{
    [Table("Application_Menus")]
    public class ApplicationMenus
    {
        [Key]
        public int Id { get; set; }
        public Guid Menu_Id { get; set; }
        public string Menu_Name { get; set; }
        public string Description { get; set; }
        public bool isEnable { get; set; }
    }
}
