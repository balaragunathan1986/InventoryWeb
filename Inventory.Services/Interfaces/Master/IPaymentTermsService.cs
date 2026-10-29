using Inventory.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.Interfaces.Master
{
    public interface IPaymentTermsService
    {
        Task<List<PaymentTermsDTO>> GetPaymentTerms();
        Task<PaymentTermsDTO> GetPaymentTerms(Guid PaymentTerms_Id);
        Task<PaymentTermsDTO> Save(PaymentTermsDTO PaymentTerms);
        Task<PaymentTermsDTO> Update(PaymentTermsDTO PaymentTerms);
        Task<bool> Delete(Guid PaymentTerms_Id);
    }
}
