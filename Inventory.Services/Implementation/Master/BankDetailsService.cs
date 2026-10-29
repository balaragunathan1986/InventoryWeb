using Inventory.Domain.DTO;
using Inventory.Repository.Interfaces.Master;
using Inventory.Services.Interfaces.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.Implementation.Master
{
    public class BankDetailsService : IBankDetailsService
    {
        private readonly IBankDetailsRepository _bankDetailsRepository;

        public BankDetailsService(IBankDetailsRepository bankDetailsRepository)
        {
            _bankDetailsRepository = bankDetailsRepository;
        }
        public async Task<IEnumerable<BankDetailsDTO>> GetBankDetailsAsync()
        {
            return await _bankDetailsRepository.GetBankDetails();
        }
        public async Task<IEnumerable<BankDetailsDTO>> GetBankDetailsByReferenceId(Guid reference_Id)
        {
            return await _bankDetailsRepository.GetBankDetailsList(reference_Id);
        }
        public async Task<BankDetailsDTO> GetBankDetailsByIdAsync(Guid id)
        {
            return await _bankDetailsRepository.GetBankDetails(id);
        }
        public async Task<BankDetailsDTO> SaveAsync(BankDetailsDTO bankDetails)
        {
            return await _bankDetailsRepository.Save(bankDetails);
        }
        public async Task<BankDetailsDTO> UpdateAsync(BankDetailsDTO bankDetails)
        {
            return await _bankDetailsRepository.Update(bankDetails);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _bankDetailsRepository.Delete(id);
        }
    }
}
