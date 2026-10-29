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
    public class ChargeService : IChargeService
    {
        private readonly IChargeRepository _chargeRepository;

        public ChargeService(IChargeRepository chargeRepository)
        {
            _chargeRepository = chargeRepository;
        }

        public async Task<List<ChargeDTO>> GetCharge()
        {
            return await _chargeRepository.GetCharge();
        }

        public async Task<ChargeDTO> GetCharge(Guid Charge_Id)
        {
            return await _chargeRepository.GetCharge(Charge_Id);
        }

        public async Task<ChargeDTO> Save(ChargeDTO Charge)
        {
            return await _chargeRepository.Save(Charge);
        }

        public async Task<ChargeDTO> Update(ChargeDTO Charge)
        {
            return await _chargeRepository.Update(Charge);
        }

        public async Task<bool> Delete(Guid Charge_Id)
        {
            return await _chargeRepository.Delete(Charge_Id);
        }
    }
}
