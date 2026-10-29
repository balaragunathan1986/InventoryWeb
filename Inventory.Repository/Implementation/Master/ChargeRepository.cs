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
    public class ChargeRepository : IChargeRepository
    {
        private readonly AppDBContext _dbcontext;
        private readonly IConfiguration _configuration;
        public ChargeRepository(AppDBContext _context, IConfiguration configuration)
        {
            _dbcontext = _context;
            _configuration = configuration;
        }

        public async Task<List<ChargeDTO>> GetCharge()
        {
            return await _dbcontext.Charge.Where(_ => !_.IsDeleted).ToListAsync();
        }

        public async Task<ChargeDTO> GetCharge(Guid Charge_Id)
        {


            var paymode = await _dbcontext.Charge.FirstOrDefaultAsync(item => item.Charge_Id.ToString().ToLower() == Charge_Id.ToString().ToLower());

            return paymode ?? new ChargeDTO();
        }

        public async Task<ChargeDTO> Save(ChargeDTO charge)
        {
            await _dbcontext.Charge.AddAsync(charge);
            await _dbcontext.SaveChangesAsync();
            return charge;
        }

        public async Task<ChargeDTO> Update(ChargeDTO charge)
        {
            var chg = await _dbcontext.Charge.FirstOrDefaultAsync(item => item.Charge_Id == charge.Charge_Id && item.Id == charge.Id && item.IsDeleted == false);
            if (chg != null)
            {
                chg.ChargeName = charge.ChargeName;
                chg.ChargeCode = charge.ChargeCode;
                chg.ChargeDescription = charge.ChargeDescription;
                chg.ChargeType = charge.ChargeType;
                chg.ChargePercentage = charge.ChargePercentage;
                chg.IsActive = charge.IsActive;
                chg.UpdatedBy = charge.UpdatedBy;
                chg.UpdatedAt = charge.UpdatedAt;
                await _dbcontext.SaveChangesAsync();
                return chg; 
            }
            return new ChargeDTO();
        }
        public async Task<bool> Delete(Guid Charge_Id)
        {
            var chg = await _dbcontext.Charge.FirstOrDefaultAsync(item => item.Charge_Id == Charge_Id && item.IsDeleted == false);
            if (chg != null)
            {
                _dbcontext.Charge.Remove(chg);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}
