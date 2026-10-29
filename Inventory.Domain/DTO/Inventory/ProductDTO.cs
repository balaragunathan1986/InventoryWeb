using Inventory.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Inventory.Domain.DTO.Inventory
{
    [Table("Product")]
    public class ProductDTO : AuditFields
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("product_Id")]
        public Guid Product_Id { get; set; }

        // Product Details
        [JsonPropertyName("productCode")]
        public string ProductCode { get; set; } = string.Empty;

        [JsonPropertyName("productName")]
        public string ProductName { get; set; } = string.Empty;

        [JsonPropertyName("productDescription")]
        public string? ProductDescription { get; set; }

        // Masters
        [JsonPropertyName("productGroupId")]
        public int? ProductGroupId { get; set; }

        [JsonPropertyName("uomId")]
        public int? UomId { get; set; }

        [JsonPropertyName("chargeId")]
        public int? ChargeId { get; set; }

        // Product Codes
        [JsonPropertyName("barcode")]
        public string? Barcode { get; set; }

        [JsonPropertyName("hsnCode")]
        public string? HSNCode { get; set; }

        // Pricing
        [JsonPropertyName("costPrice")]
        public decimal CostPrice { get; set; }

        [JsonPropertyName("salesPrice")]
        public decimal SalesPrice { get; set; }

        [JsonPropertyName("mrp")]
        public decimal MRP { get; set; }

        // Inventory
        [JsonPropertyName("stockQuantity")]
        public decimal StockQuantity { get; set; }

        // Manufacturing
        [JsonPropertyName("manufactureDate")]
        public DateTime? ManufactureDate { get; set; }

        [JsonPropertyName("expiryDate")]
        public DateTime? ExpiryDate { get; set; }

        // Image
        [JsonPropertyName("imagePath")]
        public string? ImagePath { get; set; }

        // Remarks
        [JsonPropertyName("remarks")]
        public string? Remarks { get; set; }

        // Status
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; }

    }
}



