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
    public class PaymentModeRepository : IPaymentModeRepository
    {
        private readonly AppDBContext _dbcontext;
        private readonly IConfiguration _configuration;
        public PaymentModeRepository(AppDBContext _context, IConfiguration configuration)
        {
            _dbcontext = _context;
            _configuration = configuration;
        }

        public async Task<List<PaymentModeDTO>> GetPaymentMode()
        {
            return await _dbcontext.PaymentMode.Where(_ => !_.IsDeleted).ToListAsync();
        }

        public async Task<PaymentModeDTO> GetPaymentMode(Guid PaymentMode_Id)       {
        

            var paymode = await _dbcontext.PaymentMode.FirstOrDefaultAsync(item => item.PaymentMode_Id.ToString().ToLower() == PaymentMode_Id.ToString().ToLower());
          
            return paymode ?? new PaymentModeDTO();
        }

        public async Task<PaymentModeDTO> Save(PaymentModeDTO PaymentMode)
        {
            await _dbcontext.PaymentMode.AddAsync(PaymentMode);
            await _dbcontext.SaveChangesAsync();
            return PaymentMode;
        }

        public async Task<PaymentModeDTO> Update(PaymentModeDTO PaymentMode)
        {
            var paymode = await _dbcontext.PaymentMode.FirstOrDefaultAsync(item => item.PaymentMode_Id == PaymentMode.PaymentMode_Id && item.Id == PaymentMode.Id && item.IsDeleted == false);
            if (paymode != null)
            {
                //paymode = PaymentMode;
                //_dbcontext.PaymentMode.Update(PaymentMode);
                //await _dbcontext.SaveChangesAsync();
                //return paymode;
                paymode.PaymentMode = PaymentMode.PaymentMode;
                paymode.IsActive = PaymentMode.IsActive;
                paymode.UpdatedBy = PaymentMode.UpdatedBy;
                paymode.UpdatedAt = PaymentMode.UpdatedAt;
                await _dbcontext.SaveChangesAsync();
                return paymode;
            }
            return new PaymentModeDTO();
        }
        public async Task<bool> Delete(Guid PaymentMode_Id)
        {
            var paymode = await _dbcontext.PaymentMode.FirstOrDefaultAsync(item => item.PaymentMode_Id == PaymentMode_Id && item.IsDeleted == false);
            if (paymode != null)
            {
                _dbcontext.PaymentMode.Remove(paymode);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}
