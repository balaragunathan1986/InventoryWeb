
using Inventory.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.Interfaces.Master
{
    public interface ICompanyService
    {
        Task<List<CompanyDTO>> GetCompanies();
        Task<CompanyDTO> GetCompany(Guid Company_Id);
        Task<CompanyDTO> Save(CompanyDTO Company);
        Task<CompanyDTO> Update(CompanyDTO Company);
        Task<bool> Delete(Guid Company_Id);
    }
}
