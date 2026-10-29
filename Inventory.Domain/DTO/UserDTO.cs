using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.DTO
{
    [Table("AspNetUsers")]
    public class UserDTO
    {
        [Key]
        public int Id { get; set; }
        public Guid User_Id { get; set; }
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public required string User_Name { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public bool IsActive { get; set; } = false;
        public bool? IsLocked { get; set; } = false;
        public bool? IsDeleted { get; set; } = false;
        public Guid? Role_Id { get; set; } = Guid.Empty;
        public DateTime? Last_LoggedIn { get; set; }
        public int FailedLoginAttempts { get; set; } = 0;
        public DateTime? LockoutEnd { get; set; }
    }
}
