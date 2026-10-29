using Inventory.Domain.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inventory.Domain.DTO.Inventory
{
    [Table("ProductStock")]
    public class ProductStockDTO : AuditFields
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("productStock_Id")]
        public Guid ProductStock_Id { get; set; }

        [JsonPropertyName("productStockNo")]
        public string ProductStockNo { get; set; } = string.Empty;

        [JsonPropertyName("itemCode")]
        public string ItemCode { get; set; } = string.Empty;

        [JsonPropertyName("qty")]
        public decimal Qty { get; set; }

        [JsonPropertyName("rate")]
        public decimal? Rate { get; set; }

        [JsonPropertyName("allotedQty")]
        public decimal? AllotedQty { get; set; }

        [JsonPropertyName("sQty")]
        public decimal? SQty { get; set; }

        [JsonPropertyName("balQty")]
        public decimal? BalQty { get; set; }

        [JsonPropertyName("returnQty")]
        public decimal? ReturnQty { get; set; }

        [JsonPropertyName("markupRate")]
        public decimal MarkupRate { get; set; }

        [JsonPropertyName("product_Id")]
        public Guid? Product_Id { get; set; }

        [JsonPropertyName("uom_Id")]
        public Guid? Uom_Id { get; set; }

        [JsonPropertyName("company_Id")]
        public int? Company_Id { get; set; }

        [JsonPropertyName("productStockDate")]
        public DateTime? ProductStockDate { get; set; }

        [JsonPropertyName("remarks")]
        public string? Remarks { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; }

        // Display only fields (joined from masters), not mapped to a column
        [NotMapped]
        [JsonPropertyName("productName")]
        public string? ProductName { get; set; }

        [NotMapped]
        [JsonPropertyName("uomName")]
        public string? UomName { get; set; }

        [NotMapped]
        [JsonPropertyName("companyName")]
        public string? CompanyName { get; set; }
    }
}
