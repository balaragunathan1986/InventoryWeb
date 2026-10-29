using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    [Table("Job_Receipt_Items")]
    public class JobReceiptItemDTO
    {
        [Key]
        public int Id { get; set; }
        public Guid Job_Receipt_Id { get; set; }
        public Guid Job_Receipt_Item_Id { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public string? Remarks { get; set; }
    }
}
