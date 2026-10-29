using Inventory.Domain.DTO;
using Inventory.Domain.DTO.ProductionAndLogistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Model.Production
{
    public class WagesModel
    {
        public WagesDTO? WagesMaster { get; set; }
        public List<WagesChargesDTO>? WagesDetails { get; set; }
    }
}
