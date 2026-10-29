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
    public class UomRepository : IUomRepository
    {
        private readonly AppDBContext _dbcontext;
        private readonly IConfiguration _configuration;
        public UomRepository(AppDBContext _context,IConfiguration configuration) 
        {
            _dbcontext = _context;
            _configuration = configuration;
        }

        public async Task<List<UomDTO>> GetUom()
        {
            return await _dbcontext.Uom.Where(_=>!_.IsDeleted).ToListAsync();
        }

        public async Task<UomDTO> GetUom(Guid Uom_Id)
        {
           var uom = await _dbcontext.Uom.FirstOrDefaultAsync(item => item.Uom_Id.ToString().ToLower() == Uom_Id.ToString().ToLower());
            return uom ?? new UomDTO();
        }

        public async Task<UomDTO> Save(UomDTO Uom)
        {
            await _dbcontext.Uom.AddAsync(Uom);
            await _dbcontext.SaveChangesAsync();
            return Uom;
        }

        public async Task<UomDTO> Update(UomDTO Uom)
        {
            var uom = await _dbcontext.Uom.FirstOrDefaultAsync(item => item.Uom_Id == Uom.Uom_Id && item.Id == Uom.Id && item.IsDeleted == false);
            if (uom != null)
            {
                uom.Uom = Uom.Uom;
                uom.Abbrevation = Uom.Abbrevation;
                uom.IsActive = Uom.IsActive;
                uom.UpdatedBy = Uom.UpdatedBy;
                uom.UpdatedAt = Uom.UpdatedAt;
                await _dbcontext.SaveChangesAsync();
                return uom;
            }
            return new UomDTO();
        }
        public async Task<bool> Delete(Guid Uom_Id)
        {
            var uom = await _dbcontext.Uom.FirstOrDefaultAsync(item => item.Uom_Id == Uom_Id && item.IsDeleted == false);
            if (uom != null)
            {
                _dbcontext.Uom.Remove(uom);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}
