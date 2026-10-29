using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain.Model.Account
{
    [Table("VerifyCode")]
    public class VerifyCode
    {
        [Key]
        public Guid user_Id { get; set; }
        public string? code { get; set; }
        public bool isExpired { get; set; } = false;
    }
}
