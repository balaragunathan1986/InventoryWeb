using Inventory.Domain.DTO;
using Inventory.Repository.Interfaces.Master;
using Inventory.Services.Interfaces.Master;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Services.Implementation.Master
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<List<DepartmentDTO>> GetDepartment()
        {
            return await _departmentRepository.GetDepartment();
        }

        public async Task<DepartmentDTO> GetDepartment(Guid Department_Id)
        {
            return await _departmentRepository.GetDepartment(Department_Id);
        }

        public async Task<DepartmentDTO> Save(DepartmentDTO department)
        {
            return await _departmentRepository.Save(department);
        }

        public async Task<DepartmentDTO> Update(DepartmentDTO department)
        {
            return await _departmentRepository.Update(department);
        }

        public async Task<bool> Delete(Guid Department_Id)
        {
            return await _departmentRepository.Delete(Department_Id);
        }
    }
}
