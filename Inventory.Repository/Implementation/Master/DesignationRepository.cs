using Inventory.Domain.DTO;
using Inventory.Repository.Data;
using Inventory.Repository.Interfaces.Master;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Repository.Implementation.Master
{
    public class DesignationRepository : IDesignationRepository
    {
        private readonly AppDBContext _dbcontext;

        public DesignationRepository(AppDBContext context)
        {
            _dbcontext = context;
        }

        public async Task<List<DesignationDTO>> GetDesignation()
        {
            return await _dbcontext.Designation.Where(_ => !_.IsDeleted).ToListAsync();
        }

        public async Task<DesignationDTO> GetDesignation(Guid Designation_Id)
        {
            var designation = await _dbcontext.Designation.FirstOrDefaultAsync(item => item.Designation_Id == Designation_Id);
            return designation ?? new DesignationDTO();
        }

        public async Task<DesignationDTO> Save(DesignationDTO designation)
        {
            await _dbcontext.Designation.AddAsync(designation);
            await _dbcontext.SaveChangesAsync();
            return designation;
        }

        public async Task<DesignationDTO> Update(DesignationDTO designation)
        {
            var existing = await _dbcontext.Designation.FirstOrDefaultAsync(item => item.Designation_Id == designation.Designation_Id && item.Id == designation.Id && !item.IsDeleted);
            if (existing != null)
            {
                existing.DesignationName = designation.DesignationName;
                existing.DesignationCode = designation.DesignationCode;
                existing.IsActive = designation.IsActive;
                existing.UpdatedBy = designation.UpdatedBy;
                existing.UpdatedAt = designation.UpdatedAt;
                await _dbcontext.SaveChangesAsync();
                return existing;
            }

            return new DesignationDTO();
        }

        public async Task<bool> Delete(Guid Designation_Id)
        {
            var designation = await _dbcontext.Designation.FirstOrDefaultAsync(item => item.Designation_Id == Designation_Id && !item.IsDeleted);
            if (designation != null)
            {
                _dbcontext.Designation.Remove(designation);
                await _dbcontext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
