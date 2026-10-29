using Inventory.Domain.DTO;
using Inventory.Repository.Interfaces.Master;
using Inventory.Services.Interfaces.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.Implementation.Master
{
    public class UomService : IUomService
    {
        private readonly IUomRepository _uomRepository;

        public UomService(IUomRepository uomRepository) {
            _uomRepository = uomRepository;
        }

        public async Task<List<UomDTO>> GetUom()
        {
          return await _uomRepository.GetUom();
        }

        public async Task<UomDTO> GetUom(Guid Uom_Id)
        {
            return await _uomRepository.GetUom(Uom_Id);
        }

        public async Task<UomDTO> Save(UomDTO Uom)
        {
            return await _uomRepository.Save(Uom);
        }

        public async Task<UomDTO> Update(UomDTO Uom)
        {
            return await _uomRepository.Update(Uom);
        }

        public async Task<bool> Delete(Guid Uom_Id)
        {
            return await _uomRepository.Delete(Uom_Id);
        }
    }
}
