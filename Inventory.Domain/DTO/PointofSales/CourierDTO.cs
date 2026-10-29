using Inventory.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain.DTO.PointofSales;

[Table("Courier_Master")]
public class CourierDTO : AuditFields
{
    [Key]
    public int Id { get; set; }
    public Guid Courier_Id { get; set; }
    public Guid Sales_Id { get; set; }
    [NotMapped]
    public string? Sales_Number { get; set; }
    public string? AWB { get; set; }
    public string Courier_Company { get; set; } = string.Empty;
    public string? Sender { get; set; }
    public string? Receiver { get; set; }
    public string? Origin { get; set; }
    public string? Destination { get; set; }
    public decimal? Weight { get; set; }
    public string? Dimensions { get; set; }
    public decimal? Charges { get; set; }
    public DateTime? Dispatch_Date { get; set; }
    public DateTime? Expected_Delivery_Date { get; set; }
    public string Courier_Status { get; set; } = "In Transit";
    public string? Remarks { get; set; }
    //public bool IsActive { get; set; } = true;
    //public bool IsDeleted { get; set; }
}

public class CourierSaveRequest : CourierDTO
{
}
