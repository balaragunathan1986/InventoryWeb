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
    public class CompanyService : ICompanyService   
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<List<CompanyDTO>> GetCompanies() 
        { 
            return await _companyRepository.GetCompanies();
        }
        public async Task<CompanyDTO> GetCompany(Guid Company_Id)
        {
          return await _companyRepository.GetCompany(Company_Id);
        }
        public async Task<CompanyDTO> Save(CompanyDTO Company)
        {
           return await _companyRepository.Save(Company);
        }
        public async Task<CompanyDTO> Update(CompanyDTO Company)
        {
            return await _companyRepository.Update(Company);   
        }
        public async Task<bool> Delete(Guid Company_Id)
        {
            return await _companyRepository.Delete(Company_Id);
        }

    }
}
