using Inventory.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    [Table("Job_Receipt_Master")]
    public class JobReceiptDTO : AuditFields
    {
        [Key]
        public int Id { get; set; }
        public Guid Job_Receipt_Id { get; set; }
        public string? Job_Receipt_Number { get; set; }
        public DateTime? Job_Receipt_Date { get; set; }
        public string? Supplier { get; set; }
        public Guid Supplier_Id { get; set; }    
        public string? Vehicle_Number { get; set; }
        public string? Driver_Phone { get; set; }
        public string? Driver_Name { get; set; }
        public decimal? First_Weight { get; set; }
        public decimal? Second_Weight { get; set; }
        public decimal? Net_Weight { get; set; }
        public decimal? Total_Quantity { get; set; }
        public decimal? Total_Amount { get; set; }
        public int Payment_Status { get; set; }
        public string? Location { get; set; } = string.Empty;
        public decimal? Transport_Charges { get; set; } = 0;
    }
}
