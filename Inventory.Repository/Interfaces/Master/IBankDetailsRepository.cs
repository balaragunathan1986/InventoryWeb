using Inventory.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repository.Interfaces.Master
{
    public interface IBankDetailsRepository
    {
        Task<List<BankDetailsDTO>> GetBankDetails();
        Task<BankDetailsDTO> GetBankDetails(Guid Bank_Id);
        Task<IList<BankDetailsDTO>> GetBankDetailsList(Guid Reference_Id);
        Task<BankDetailsDTO> Save(BankDetailsDTO BankDetails);
        Task<BankDetailsDTO> Update(BankDetailsDTO BankDetails);
        Task<bool> Delete(Guid Bank_Id);
    }
}
