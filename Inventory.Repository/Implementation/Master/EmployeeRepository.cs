using Inventory.Domain.DTO;
using Inventory.Repository.Data;
using Inventory.Repository.Interfaces.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Repository.Implementation.Master
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDBContext _dbcontext;
        private readonly IConfiguration _configuration;

        public EmployeeRepository(AppDBContext context, IConfiguration configuration)
        {
            _dbcontext = context;
            _configuration = configuration;
        }

        public async Task<List<EmployeeDTO>> GetEmployee()
        {
            return await _dbcontext.Employee.Where(_ => !_.IsDeleted).ToListAsync();
        }

        public async Task<EmployeeDTO> GetEmployee(Guid Employee_Id)
        {
            var employee = await _dbcontext.Employee.FirstOrDefaultAsync(item => item.Employee_Id == Employee_Id);
            return employee ?? new EmployeeDTO();
        }

        public async Task<EmployeeDTO> Save(EmployeeDTO employee)
        {
            try
            {
                employee.DepartmentId = employee.DepartmentId == 0 ? null : employee.DepartmentId;
                employee.DesignationId = employee.DesignationId == 0 ? null : employee.DesignationId;
                employee.EmployeeStatusId = employee.EmployeeStatusId == 0 ? null : employee.EmployeeStatusId;

                await _dbcontext.Employee.AddAsync(employee);

                var rowsAffected = await _dbcontext.SaveChangesAsync();

                if (rowsAffected == 0)
                {
                    throw new Exception("Employee was not inserted.");
                }

                return employee;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"DbUpdateException: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");

                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Type: {ex.GetType().Name}");
                Console.WriteLine($"Message: {ex.Message}");

                throw;
            }
        }

        public async Task<EmployeeDTO> Update(EmployeeDTO employee)
        {
            var existing = await _dbcontext.Employee.FirstOrDefaultAsync(item => item.Employee_Id == employee.Employee_Id && item.Id == employee.Id && item.IsDeleted == false);
            if (existing != null)
            {
                existing.EmployeeCode = employee.EmployeeCode;
                existing.FirstName = employee.FirstName;
                existing.MiddleName = employee.MiddleName;
                existing.LastName = employee.LastName;
                existing.DisplayName = employee.DisplayName;
                existing.DateOfBirth = employee.DateOfBirth;
                existing.Email = employee.Email;
                existing.MobileNumber = employee.MobileNumber;
                existing.AddressLine1 = employee.AddressLine1;
                existing.AddressLine2 = employee.AddressLine2;
                existing.AddressLine3 = employee.AddressLine3;
                existing.City = employee.City;
                existing.District = employee.District;
                existing.PostalCode = employee.PostalCode;
                existing.Landmark = employee.Landmark;
                existing.DepartmentId = employee.DepartmentId == 0 ? null : employee.DepartmentId;
                existing.DesignationId = employee.DesignationId == 0 ? null : employee.DesignationId;
                existing.EmployeeStatusId = employee.EmployeeStatusId == 0 ? null : employee.EmployeeStatusId;
                existing.JoiningDate = employee.JoiningDate;
                existing.ConfirmationDate = employee.ConfirmationDate;
                existing.ResignationDate = employee.ResignationDate;
                existing.LastWorkingDate = employee.LastWorkingDate;
                existing.ProfileImagePath = employee.ProfileImagePath;
                existing.Remarks = employee.Remarks;
                existing.IsActive = employee.IsActive;
                existing.UpdatedBy = employee.UpdatedBy;
                existing.UpdatedAt = employee.UpdatedAt;
                await _dbcontext.SaveChangesAsync();
                return existing;
            }
            return new EmployeeDTO();
        }

        public async Task<bool> Delete(Guid Employee_Id)
        {
            var employee = await _dbcontext.Employee.FirstOrDefaultAsync(item => item.Employee_Id == Employee_Id && item.IsDeleted == false);
            if (employee != null)
            {
                _dbcontext.Employee.Remove(employee);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
