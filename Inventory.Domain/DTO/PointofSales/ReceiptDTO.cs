using Inventory.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain.DTO.PointofSales;

[Table("Receipt_Master")]
public class ReceiptDTO : AuditFields
{
    [Key]
    public long Id { get; set; }
    public Guid Receipt_Id { get; set; }
    public string Receipt_Number { get; set; } = string.Empty;
    public DateTime? Receipt_Date { get; set; }
    public int? Customer_Id { get; set; }
    [NotMapped]
    public string? Customer_Name { get; set; }
    public int? Company_Id { get; set; }
    public Guid? PaymentMode_Id { get; set; }
    [NotMapped]
    public string? PaymentMode_Name { get; set; }
    public string? Reference_Number { get; set; }
    public decimal Amount { get; set; }
    public string? Remarks { get; set; }
    [NotMapped]
    public List<ReceiptAllocationDTO> Allocations { get; set; } = new();
}

[Table("Receipt_Allocation")]
public class ReceiptAllocationDTO : AuditFields
{
    [Key]
    public long Id { get; set; }
    public Guid Receipt_Allocation_Id { get; set; }
    public Guid Receipt_Id { get; set; }
    public Guid Sales_Id { get; set; }
    [NotMapped]
    public string? Sales_Number { get; set; }
    public decimal Allocated_Amount { get; set; }
}

public class ReceiptAllocationSaveRequest : ReceiptAllocationDTO { }

public class ReceiptSaveRequest : ReceiptDTO
{
    public List<ReceiptAllocationSaveRequest> Receipt_Allocations { get; set; } = new();
}