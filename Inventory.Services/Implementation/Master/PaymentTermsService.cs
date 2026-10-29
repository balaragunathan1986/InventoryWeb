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
    public class PaymentTermsService : IPaymentTermsService
    {
        private readonly IPaymentTermsRepository _paymentTermsRepository;

        public PaymentTermsService(IPaymentTermsRepository paymentTermsRepository)
        {
            _paymentTermsRepository = paymentTermsRepository;
        }

        public async Task<List<PaymentTermsDTO>> GetPaymentTerms()
        {
            return await _paymentTermsRepository.GetPaymentTerms();
        }

        public async Task<PaymentTermsDTO> GetPaymentTerms(Guid PayTerm_Id)
        {
            return await _paymentTermsRepository.GetPaymentTerms(PayTerm_Id);
        }

        public async Task<PaymentTermsDTO> Save(PaymentTermsDTO PayTerm)
        {
            return await _paymentTermsRepository.Save(PayTerm);
        }

        public async Task<PaymentTermsDTO> Update(PaymentTermsDTO PayTerm)
        {
            return await _paymentTermsRepository.Update(PayTerm);
        }

        public async Task<bool> Delete(Guid PayTerm_Id)
        {
            return await _paymentTermsRepository.Delete(PayTerm_Id);
        }
    }
}
