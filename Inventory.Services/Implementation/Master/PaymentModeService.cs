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
    public class PaymentModeService : IPaymentModeService
    {
        private readonly IPaymentModeRepository _paymentModeRepository;

        public PaymentModeService(IPaymentModeRepository paymentModeRepository)
        {
            _paymentModeRepository = paymentModeRepository;
        }

        public async Task<List<PaymentModeDTO>> GetPaymentMode()
        {
            return await _paymentModeRepository.GetPaymentMode();
        }

        public async Task<PaymentModeDTO> GetPaymentMode(Guid PayMode_Id)
        {
            return await _paymentModeRepository.GetPaymentMode(PayMode_Id);
        }

        public async Task<PaymentModeDTO> Save(PaymentModeDTO PayMode)
        {
            return await _paymentModeRepository.Save(PayMode);
        }

        public async Task<PaymentModeDTO> Update(PaymentModeDTO PayMode)
        {
            return await _paymentModeRepository.Update(PayMode);
        }

        public async Task<bool> Delete(Guid Uom_Id)
        {
            return await _paymentModeRepository.Delete(Uom_Id);
        }
    }
}
