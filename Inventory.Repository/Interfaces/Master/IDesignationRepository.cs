using Inventory.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Repository.Interfaces.Master
{
    public interface IDesignationRepository
    {
        Task<List<DesignationDTO>> GetDesignation();
        Task<DesignationDTO> GetDesignation(Guid Designation_Id);
        Task<DesignationDTO> Save(DesignationDTO designation);
        Task<DesignationDTO> Update(DesignationDTO designation);
        Task<bool> Delete(Guid Designation_Id);
    }
}
