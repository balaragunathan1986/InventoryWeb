using Inventory.Domain.DTO;
using Inventory.Domain.Model;
using Inventory.Services.Interfaces.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers.v1.Master
{
    [Route("api/v1/masters/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try {
                var companyList = await _companyService.GetCompanies();
                return Ok(ApiResponse<object>.SuccessResponse(companyList, "Companies are fetched successfully..!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse( ex.Message, "Failed to fetch companies."));
            }
        
        }

        [HttpGet("{Company_Id}")]
        public async Task<IActionResult> Get(Guid Company_Id)
        {
            try
            {
                var company = await _companyService.GetCompany(Company_Id);
                return Ok(ApiResponse<object>.SuccessResponse(company, "Company is fetched successfully..!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(ex.Message, "Failed to fetch company."));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompanyDTO Company)
        {
            try
            {
                var company = await _companyService.Save(Company);
                return Ok(ApiResponse<object>.SuccessResponse(company, "Company is saved successfully..!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(ex.Message, "Failed to save company."));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CompanyDTO Company)
        {
            try
            {
                var company = await _companyService.Update(Company);
                return Ok(ApiResponse<object>.SuccessResponse(company, "Company is updated successfully..!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(ex.Message, "Failed to update company."));
            }
        }

        [HttpDelete("{Company_Id}")]
        public async Task<IActionResult> Delete(Guid Company_Id)
        {
            try
            {
                await _companyService.Delete(Company_Id);
                return Ok(ApiResponse<string>.SuccessResponse("", "Company is deleted successfully..!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(ex.Message, "Failed to delete company."));
            }
        }
    }
}
