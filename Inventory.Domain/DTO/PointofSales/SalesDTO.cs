using Inventory.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inventory.Domain.DTO.PointofSales;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SalesChargeType
{
    Percentage,
    Amount
}

[Table("Sales_Detail")]
public class SalesDetailDTO : AuditFields
{
    [Key]
    public long Id { get; set; }
    public Guid Sales_Detail_Id { get; set; }
    public Guid Sales_Id { get; set; }
    public Guid Product_Id { get; set; }
    [NotMapped]
    public string? Product_Name { get; set; }
    public Guid UOM_Id { get; set; }
    [NotMapped]
    public string? UOM_Name { get; set; }
    public decimal Quantity { get; set; }
    [NotMapped]
    public decimal Available_Quantity { get; set; }
    public decimal Rate { get; set; }
    public decimal Gross_Amount { get; set; }
    public string? Remarks { get; set; }
    [NotMapped]
    public List<SalesStockAllocationDTO> Stock_Allocations { get; set; } = new();
}

[Table("Sales_Stock_Allocation")]
public class SalesStockAllocationDTO : AuditFields
{
    [Key]
    public long Id { get; set; }
    public Guid Sales_Stock_Allocation_Id { get; set; }
    public Guid Sales_Detail_Id { get; set; }
    public Guid Stock_Id { get; set; }
    public Guid? Batch_Id { get; set; }
    public decimal Allocated_Qty { get; set; }
}

[Table("Sales_Charge")]
public class SalesChargeDTO : AuditFields
{
    [Key]
    public int Id { get; set; }
    public Guid Sales_Charge_Id { get; set; }
    public Guid Sales_Id { get; set; }
    public Guid Charge_Id { get; set; }
    [NotMapped]
    public string? Charge_Name { get; set; }
    public SalesChargeType Charge_Type { get; set; }
    public decimal? Charge_Percentage { get; set; }
    public decimal Charge_Amount { get; set; }
    public string? Remarks { get; set; }
}

[Table("Sales_Master")]
public class SalesDTO : AuditFields
{
    [Key]
    public int Id { get; set; }
    public Guid Sales_Id { get; set; }
    public string Sales_Number { get; set; } = string.Empty;
    public DateTime? Sales_Date { get; set; }
    public int? Customer_Id { get; set; }
    [NotMapped]
    public string? Customer_Name { get; set; }
    public string? Sales_Type { get; set; }
    public int? Company_Id { get; set; }
    //public Guid? PaymentMode_Id { get; set; }
    [NotMapped]
    public string? PaymentMode_Name { get; set; }
    public Guid? PaymentTerms_Id { get; set; }
    [NotMapped]
    public string? PaymentTerms_Name { get; set; }
    public decimal Sub_Total { get; set; }
    public decimal Charge_Total { get; set; }
    public decimal Net_Amount { get; set; }
    public decimal Paid_Amount { get; set; }
    public decimal Balance_Amount { get; set; }
    public string Payment_Status { get; set; } = "Unpaid";
    public string? Remarks { get; set; }
    public string? Terms_Conditions { get; set; }
    //public bool IsActive { get; set; }
    //public bool IsDeleted { get; set; }
    [NotMapped]
    public List<SalesDetailDTO> Details { get; set; } = new();
    [NotMapped]
    public List<SalesChargeDTO> Charges { get; set; } = new();
}

public class SalesDetailSaveRequest : SalesDetailDTO { }
public class SalesChargeSaveRequest : SalesChargeDTO { }

public class SalesSaveRequest : SalesDTO
{
    public List<SalesDetailSaveRequest> Sales_Details { get; set; } = new();
    public List<SalesChargeSaveRequest> Sales_Charges { get; set; } = new();
}
