using Inventory.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repository.Interfaces.Master
{
    public interface IPaymentModeRepository
    {
        Task<List<PaymentModeDTO>> GetPaymentMode();
        Task<PaymentModeDTO> GetPaymentMode(Guid PaymentMode_Id);
        Task<PaymentModeDTO> Save(PaymentModeDTO PayMode);
        Task<PaymentModeDTO> Update(PaymentModeDTO PayMode);
        Task<bool> Delete(Guid PaymentMode_Id);
    }
}
