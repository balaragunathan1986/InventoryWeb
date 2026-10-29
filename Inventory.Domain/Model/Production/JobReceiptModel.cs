using Inventory.Domain.DTO.ProductionAndLogistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Model.Production
{
    public class JobReceiptModel
    {
        public JobReceiptDTO? JobReceipt { get; set; }
        public List<JobReceiptItemDTO>? JobReceiptItems { get; set; }
        public List<JobReceiptChargeDTO>? JobReceiptCharges { get; set; }
    }
}
