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
    public class SalesTypeRepository : ISalesTypeRepository
    {
        private readonly AppDBContext _dbcontext;
        private readonly IConfiguration _configuration;
        public SalesTypeRepository(AppDBContext _context, IConfiguration configuration)
        {
            _dbcontext = _context;
            _configuration = configuration;
        }

        public async Task<List<SalesTypeDTO>> GetSalesType()
        {
            return await _dbcontext.SalesType.Where(_ => !_.IsDeleted).ToListAsync();
        }

        public async Task<SalesTypeDTO> GetSalesType(Guid SalesType_Id)
        {


            var paymode = await _dbcontext.SalesType.FirstOrDefaultAsync(item => item.SalesType_Id.ToString().ToLower() == SalesType_Id.ToString().ToLower());

            return paymode ?? new SalesTypeDTO();
        }

        public async Task<SalesTypeDTO> Save(SalesTypeDTO SalesType)
        {
            await _dbcontext.SalesType.AddAsync(SalesType);
            await _dbcontext.SaveChangesAsync();
            return SalesType;
        }

        public async Task<SalesTypeDTO> Update(SalesTypeDTO SalesType)
        {
            var salestype = await _dbcontext.SalesType.FirstOrDefaultAsync(item => item.SalesType_Id == SalesType.SalesType_Id && item.Id == SalesType.Id && item.IsDeleted == false);
            if (salestype != null)
            {
                salestype.SalesType = SalesType.SalesType;
                salestype.IsActive = SalesType.IsActive;
                salestype.UpdatedBy = SalesType.UpdatedBy;
                salestype.UpdatedAt = SalesType.UpdatedAt;
                await _dbcontext.SaveChangesAsync();
                return salestype;
            }
            return new SalesTypeDTO();
        }
        public async Task<bool> Delete(Guid SalesType_Id)
        {
            var saltype = await _dbcontext.SalesType.FirstOrDefaultAsync(item => item.SalesType_Id == SalesType_Id && item.IsDeleted == false);
            if (saltype != null)
            {
                _dbcontext.SalesType.Remove(saltype);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}
