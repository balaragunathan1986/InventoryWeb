using Inventory.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.Interfaces.Master
{
    public interface IBankDetailsService
    {
        Task<BankDetailsDTO> GetBankDetailsByIdAsync(Guid id);
        Task<IEnumerable<BankDetailsDTO>> GetBankDetailsByReferenceId(Guid reference_Id);
        Task<IEnumerable<BankDetailsDTO>> GetBankDetailsAsync();
        Task<BankDetailsDTO> SaveAsync(BankDetailsDTO bankDetails);
        Task<BankDetailsDTO> UpdateAsync(BankDetailsDTO bankDetails);
        Task<bool> DeleteAsync(Guid id);
    }
}
