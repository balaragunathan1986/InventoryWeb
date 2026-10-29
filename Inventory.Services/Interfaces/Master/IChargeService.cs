using Inventory.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.Interfaces.Master
{
    public interface IChargeService
    {
        Task<List<ChargeDTO>> GetCharge();
        Task<ChargeDTO> GetCharge(Guid Charge_Id);
        Task<ChargeDTO> Save(ChargeDTO Charge);
        Task<ChargeDTO> Update(ChargeDTO Charge);
        Task<bool> Delete(Guid Charge_Id);
    }
}
