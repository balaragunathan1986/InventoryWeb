using Inventory.Domain.DTO;
using Inventory.Repository.Data;
using Inventory.Repository.Interfaces.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repository.Implementation.Master
{
    public class PaymentTermsRepository : IPaymentTermsRepository
    {
        private readonly AppDBContext _dbcontext;
        private readonly IConfiguration _configuration;
        public PaymentTermsRepository(AppDBContext _context, IConfiguration configuration)
        {
            _dbcontext = _context;
            _configuration = configuration;
        }

        public async Task<List<PaymentTermsDTO>> GetPaymentTerms()
        {
            return await _dbcontext.PaymentTerms.Where(_ => !_.IsDeleted).ToListAsync();
        }

        public async Task<PaymentTermsDTO> GetPaymentTerms(Guid PaymentTerms_Id)
        {


            var paymode = await _dbcontext.PaymentTerms.FirstOrDefaultAsync(item => item.PaymentTerms_Id.ToString().ToLower() == PaymentTerms_Id.ToString().ToLower());

            return paymode ?? new PaymentTermsDTO();
        }

        public async Task<PaymentTermsDTO> Save(PaymentTermsDTO PaymentTerms)
        {
            await _dbcontext.PaymentTerms.AddAsync(PaymentTerms);
            await _dbcontext.SaveChangesAsync();
            return PaymentTerms;
        }

        public async Task<PaymentTermsDTO> Update(PaymentTermsDTO PaymentTerms)
        {
            var paymode = await _dbcontext.PaymentTerms.FirstOrDefaultAsync(item => item.PaymentTerms_Id == PaymentTerms.PaymentTerms_Id && item.Id == PaymentTerms.Id && item.IsDeleted == false);
            if (paymode != null)
            {              
                paymode.PaymentTerms = PaymentTerms.PaymentTerms;
                paymode.IsActive = PaymentTerms.IsActive;
                paymode.UpdatedBy = PaymentTerms.UpdatedBy;
                paymode.UpdatedAt = PaymentTerms.UpdatedAt;
                await _dbcontext.SaveChangesAsync();
                return paymode;
            }
            return new PaymentTermsDTO();
        }
        public async Task<bool> Delete(Guid PaymentTerms_Id)
        {
            var payterm = await _dbcontext.PaymentTerms.FirstOrDefaultAsync(item => item.PaymentTerms_Id == PaymentTerms_Id && item.IsDeleted == false);
            if (payterm != null)
            {
                _dbcontext.PaymentTerms.Remove(payterm);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}
