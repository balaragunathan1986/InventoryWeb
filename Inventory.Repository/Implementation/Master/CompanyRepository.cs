using Inventory.Domain.DTO;
using Inventory.Repository.Data;
using Inventory.Repository.Interfaces.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repository.Implementation.Master
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDBContext _dbcontext;
        private readonly IConfiguration _configuration;
        public CompanyRepository(AppDBContext _context, IConfiguration configuration)
        {
            _dbcontext = _context;
            _configuration = configuration;
        }

        public async Task<List<CompanyDTO>> GetCompanies()
        {
            return await _dbcontext.Company.Where(_ => !_.IsDeleted).ToListAsync();
        }

        public async Task<CompanyDTO> GetCompany(Guid Company_Id)
        {
            return await _dbcontext.Company.FirstOrDefaultAsync(item => item.Company_Id.ToString().ToLower() == Company_Id.ToString().ToLower()) ?? new CompanyDTO();
        }

        public async Task<CompanyDTO> Save(CompanyDTO Company)
        {
             _dbcontext.Company.Add(Company);
            await _dbcontext.SaveChangesAsync();
            return Company;
        }

        public async Task<CompanyDTO> Update(CompanyDTO Company)
        {
            var companyItem = await _dbcontext.Company.FirstOrDefaultAsync(item => item.Company_Id == Company.Company_Id && item.Id == Company.Id && item.IsDeleted == false);
            if (companyItem != null)
            {
                companyItem.Company_Name = Company.Company_Name;
                companyItem.Address1 = Company.Address1;
                companyItem.Address2 = Company.Address2;
                companyItem.City = Company.City;
                companyItem.Company_State = Company.Company_State;
                companyItem.Country = Company.Country;
                companyItem.Phone_Number = Company.Phone_Number;
                companyItem.Email = Company.Email;
                companyItem.Gst_Number = Company.Gst_Number;
                companyItem.IsActive = Company.IsActive;
                companyItem.UpdatedBy = Company.UpdatedBy;
                companyItem.UpdatedAt = Company.UpdatedAt;
                await _dbcontext.SaveChangesAsync();
                return companyItem;
            }
            return new CompanyDTO();

        }

        public async Task<bool> Delete(Guid Company_Id)
        {
            var company = await _dbcontext.Company.FirstOrDefaultAsync(item => item.Company_Id == Company_Id && item.IsDeleted == false);
            if (company != null)
            {
                _dbcontext.Company.Remove(company);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}
