using Inventory.Domain.DTO;
using Inventory.Repository.Data;
using Inventory.Repository.Interfaces.Master;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repository.Implementation.Master
{
    public class BankDetailsRepository : IBankDetailsRepository
    {
        private readonly AppDBContext _dbcontext;
        public BankDetailsRepository(AppDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<BankDetailsDTO>> GetBankDetails()
        {
            return await _dbcontext.BankDetails.ToListAsync();
        }
        public async Task<BankDetailsDTO> GetBankDetails(Guid id)
        {
            var bankDetail = await _dbcontext.BankDetails.FirstOrDefaultAsync(x => x.Bank_Id == id);
            if (bankDetail == null)
                throw new InvalidOperationException("Bank details not found");
            return bankDetail;
        }
        public async Task<IList<BankDetailsDTO>> GetBankDetailsList(Guid referenceId)
        {
            return await _dbcontext.BankDetails.Where(x => x.Reference_Id == referenceId).ToListAsync();
        }
        public async Task<BankDetailsDTO> Save(BankDetailsDTO bankDetails)
        {
            await _dbcontext.BankDetails.AddAsync(bankDetails);
            await _dbcontext.SaveChangesAsync();
            return bankDetails;

        }
        public async Task<BankDetailsDTO> Update(BankDetailsDTO bankDetails)
        {
            _dbcontext.BankDetails.Update(bankDetails);
            await _dbcontext.SaveChangesAsync();
            return bankDetails;
        }
        public async Task<bool> Delete(Guid id)
        {
            var bankDetails = await _dbcontext.BankDetails.FirstOrDefaultAsync(x => x.Bank_Id == id);
            if (bankDetails == null)
                return false;

            _dbcontext.BankDetails.Remove(bankDetails);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
    }
}
