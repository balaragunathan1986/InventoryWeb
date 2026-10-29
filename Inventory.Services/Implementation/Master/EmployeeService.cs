using Inventory.Domain.DTO;
using Inventory.Repository.Interfaces.Master;
using Inventory.Services.Interfaces.Master;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Services.Implementation.Master
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeDTO>> GetEmployee()
        {
            return await _employeeRepository.GetEmployee();
        }

        public async Task<EmployeeDTO> GetEmployee(Guid Employee_Id)
        {
            return await _employeeRepository.GetEmployee(Employee_Id);
        }

        public async Task<EmployeeDTO> Save(EmployeeDTO employee)
        {
            return await _employeeRepository.Save(employee);
        }

        public async Task<EmployeeDTO> Update(EmployeeDTO employee)
        {
            return await _employeeRepository.Update(employee);
        }

        public async Task<bool> Delete(Guid Employee_Id)
        {
            return await _employeeRepository.Delete(Employee_Id);
        }
    }
}
