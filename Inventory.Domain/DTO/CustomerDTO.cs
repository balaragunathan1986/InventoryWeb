using Inventory.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain.DTO;

[Table("Customer")]
public class CustomerDTO : AuditFields
{
    [Key]
    public int Id { get; set; }
    public Guid Customer_Id { get; set; }
    public string Customer_Name { get; set; } = string.Empty;
    public string Address1 { get; set; } = string.Empty;
    public string Address2 { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Customer_State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Phone_Number { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Gst_Number { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}
