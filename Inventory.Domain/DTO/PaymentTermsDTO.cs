using Inventory.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.DTO
{
    [Table("Payment_Terms")]
    public class PaymentTermsDTO : AuditFields
    {
        [Key]
        public int Id { get; set; }
        public Guid PaymentTerms_Id { get; set; }
        public string? PaymentTerms { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
