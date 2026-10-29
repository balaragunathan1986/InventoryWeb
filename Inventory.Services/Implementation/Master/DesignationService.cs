using Inventory.Domain.DTO;
using Inventory.Repository.Interfaces.Master;
using Inventory.Services.Interfaces.Master;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Services.Implementation.Master
{
    public class DesignationService : IDesignationService
    {
        private readonly IDesignationRepository _designationRepository;

        public DesignationService(IDesignationRepository designationRepository)
        {
            _designationRepository = designationRepository;
        }

        public async Task<List<DesignationDTO>> GetDesignation()
        {
            return await _designationRepository.GetDesignation();
        }

        public async Task<DesignationDTO> GetDesignation(Guid Designation_Id)
        {
            return await _designationRepository.GetDesignation(Designation_Id);
        }

        public async Task<DesignationDTO> Save(DesignationDTO designation)
        {
            return await _designationRepository.Save(designation);
        }

        public async Task<DesignationDTO> Update(DesignationDTO designation)
        {
            return await _designationRepository.Update(designation);
        }

        public async Task<bool> Delete(Guid Designation_Id)
        {
            return await _designationRepository.Delete(Designation_Id);
        }
    }
}
