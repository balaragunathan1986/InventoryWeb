using Inventory.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.Interfaces.Master
{
    public interface IPaymentModeService
    {
        Task<List<PaymentModeDTO>> GetPaymentMode();
        Task<PaymentModeDTO> GetPaymentMode(Guid PaymentMode_Id);
        Task<PaymentModeDTO> Save(PaymentModeDTO PaymentMode);
        Task<PaymentModeDTO> Update(PaymentModeDTO PaymentMode);
        Task<bool> Delete(Guid PaymentMode_Id);
    }
}
