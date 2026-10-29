using Inventory.Domain.DTO;
using Inventory.Domain.Model;
using Inventory.Services.Interfaces.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers.v1.Master
{
    [Route("api/v1/masters/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankDetailsService _bankDetailsService;
        public BankController(IBankDetailsService bankDetailsService)
        {
            _bankDetailsService = bankDetailsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var bankDetailsList = await _bankDetailsService.GetBankDetailsAsync();
                return Ok(ApiResponse<object>.SuccessResponse(bankDetailsList, "Bank details are fetched successfully..!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(ex.Message, "Failed to fetch bank details."));
            }

        }

        [HttpGet("{Bank_Id}")]
        public async Task<IActionResult> Get(Guid Bank_Id)
        {
            try
            {
                var bankDetails = await _bankDetailsService.GetBankDetailsByIdAsync(Bank_Id);
                return Ok(ApiResponse<object>.SuccessResponse(bankDetails, "Bank details are fetched successfully..!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(ex.Message, "Failed to fetch bank details."));
            }
        }
        [HttpGet("GetBankDetailsByReferenceId/{Reference_Id}")]
        public async Task<IActionResult> GetBankDetailsByReferenceId(Guid Reference_Id)
        {
            try
            {
                var bankDetails = await _bankDetailsService.GetBankDetailsByReferenceId(Reference_Id);
                return Ok(ApiResponse<object>.SuccessResponse(bankDetails, "Bank details are fetched successfully..!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(ex.Message, "Failed to fetch bank details."));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BankDetailsDTO BankDetails)
        {
            try
            {
                var bankDetails = await _bankDetailsService.SaveAsync(BankDetails);
                return Ok(ApiResponse<object>.SuccessResponse(bankDetails, "Bank details are saved successfully..!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(ex.Message, "Failed to save bank details."));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BankDetailsDTO BankDetails)
        {
            try
            {
                var bankDetails = await _bankDetailsService.UpdateAsync(BankDetails);
                return Ok(ApiResponse<object>.SuccessResponse(bankDetails, "Bank details are updated successfully..!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(ex.Message, "Failed to update bank details."));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Bank_Id)
        {
            try
            {
                await _bankDetailsService.DeleteAsync(Bank_Id);
                return Ok(ApiResponse<string>.SuccessResponse("", "Bank details are deleted successfully..!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(ex.Message, "Failed to delete bank details."));
            }
        }

    }
}
