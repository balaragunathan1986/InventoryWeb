using Inventory.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain.DTO.PointofSales;

public enum SalesReturnType
{
    Full,
    Partial
}

[Table("Sales_Return_Detail")]
public class SalesReturnDetailDTO : AuditFields
{
    [Key]
    public long Id { get; set; }
    public Guid Sales_Return_Detail_Id { get; set; }
    public Guid Sales_Return_Id { get; set; }
    public Guid Sales_Detail_Id { get; set; }
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
    public string? Return_Reason { get; set; }
    public string? Remarks { get; set; }
    [NotMapped]
    public List<SalesReturnStockDTO> Stocks { get; set; } = new();
}

[Table("Sales_Return_Master")]
public class SalesReturnDTO : AuditFields
{
    [Key]
    public int Id { get; set; }
    public Guid Sales_Return_Id { get; set; }
    public string Sales_Return_Number { get; set; } = string.Empty;
    public DateTime? Sales_Return_Date { get; set; }
    public Guid Sales_Id { get; set; }
    [NotMapped]
    public string? Sales_Number { get; set; }
    public int? Customer_Id { get; set; }
    [NotMapped]
    public string? Customer_Name { get; set; }
    public string? Return_Type { get; set; }
    public int? Company_Id { get; set; }
    public decimal Sub_Total { get; set; }
    public decimal Charge_Total { get; set; }
    public decimal Net_Amount { get; set; }
    public string? Remarks { get; set; }
    [NotMapped]
    public List<SalesReturnDetailDTO> Details { get; set; } = new();
}
[Table("Sales_Return_Stock")]
public class SalesReturnStockDTO : AuditFields
{
    [Key]
    public long Id { get; set; }
    public Guid Sales_Return_Stock_Id { get; set; }
    public Guid Sales_Return_Detail_Id { get; set; }
    public Guid? Stock_Id { get; set; }
    public Guid? Batch_Id { get; set; }
    public decimal Returned_Qty { get; set; }
    public string? Stock_Condition { get; set; }
    public string? Remarks { get; set; }
}

public class SalesReturnDetailSaveRequest : SalesReturnDetailDTO { }

public class SalesReturnSaveRequest : SalesReturnDTO
{
    public List<SalesReturnDetailSaveRequest> Sales_Return_Details { get; set; } = new();
    public List<SalesReturnStockSaveRequest> Sales_Return_Stocks { get; set; } = new();
}

public class SalesReturnStockSaveRequest : SalesReturnStockDTO
{
    public string ProductStockNo { get; set; } = string.Empty;
}