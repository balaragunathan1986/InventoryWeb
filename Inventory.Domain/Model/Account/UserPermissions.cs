
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain.Model.Account
{
    [Table("User_Menu_Rights")]
    public class UserPermissions
    {
        [Key]
        public int Id { get; set; }
        public Guid Role_Id { get; set; }
        public Guid? Menu_Id { get; set; }
        public string? Menu_Name { get; set; }
        public string? Description { get; set; }
        public bool isEnable {  get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
