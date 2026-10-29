using Inventory.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Services.Interfaces.Master
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDTO>> GetDepartment();
        Task<DepartmentDTO> GetDepartment(Guid Department_Id);
        Task<DepartmentDTO> Save(DepartmentDTO department);
        Task<DepartmentDTO> Update(DepartmentDTO department);
        Task<bool> Delete(Guid Department_Id);
    }
}
