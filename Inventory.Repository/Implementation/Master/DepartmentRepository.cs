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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDBContext _dbcontext;

        public DepartmentRepository(AppDBContext context)
        {
            _dbcontext = context;
        }

        public async Task<List<DepartmentDTO>> GetDepartment()
        {
            return await _dbcontext.Department.Where(_ => !_.IsDeleted).ToListAsync();
        }

        public async Task<DepartmentDTO> GetDepartment(Guid Department_Id)
        {
            var department = await _dbcontext.Department.FirstOrDefaultAsync(item => item.Department_Id == Department_Id);
            return department ?? new DepartmentDTO();
        }

        public async Task<DepartmentDTO> Save(DepartmentDTO department)
        {
            await _dbcontext.Department.AddAsync(department);
            await _dbcontext.SaveChangesAsync();
            return department;
        }

        public async Task<DepartmentDTO> Update(DepartmentDTO department)
        {
            var existing = await _dbcontext.Department.FirstOrDefaultAsync(item => item.Department_Id == department.Department_Id && item.Id == department.Id && !item.IsDeleted);
            if (existing != null)
            {
                existing.DepartmentName = department.DepartmentName;
                existing.DepartmentCode = department.DepartmentCode;
                existing.IsActive = department.IsActive;
                existing.UpdatedBy = department.UpdatedBy;
                existing.UpdatedAt = department.UpdatedAt;
                await _dbcontext.SaveChangesAsync();
                return existing;
            }

            return new DepartmentDTO();
        }

        public async Task<bool> Delete(Guid Department_Id)
        {
            var department = await _dbcontext.Department.FirstOrDefaultAsync(item => item.Department_Id == Department_Id && !item.IsDeleted);
            if (department != null)
            {
                _dbcontext.Department.Remove(department);
                await _dbcontext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
