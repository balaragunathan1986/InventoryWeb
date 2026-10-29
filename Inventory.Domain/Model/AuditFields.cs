using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Model
{
    public class AuditFields
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; } 
        public string? UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; }
        public string? DeletedBy {  get; set; }  = string.Empty;
        public DateTime? DeletedAt { get; set; }
    }
}
