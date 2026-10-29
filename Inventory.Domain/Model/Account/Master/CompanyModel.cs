using Inventory.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Model.Account.Master
{
    public class CompanyModel : CompanyDTO 
    {
        public IList<BankDetailsDTO>? BankDetails { get; set; }
    }
}
