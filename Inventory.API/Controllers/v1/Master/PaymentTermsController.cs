using Inventory.Domain.DTO;
using Inventory.Domain.Model;
using Inventory.Services.Interfaces.Master;
using Inventory.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers.v1.Master
{
    [Route("api/v1/masters/[controller]")]
    [ApiController]
    public class PaymentTermsController : ControllerBase
    {
        private readonly IPaymentTermsService _paymentTermsService;

        public PaymentTermsController(IPaymentTermsService payTermsService)
        {
            _paymentTermsService = payTermsService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var paymentTermsList = await _paymentTermsService.GetPaymentTerms();
                return Ok(ApiResponse<object>.SuccessResponse(paymentTermsList, "PaymentTerms are fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message,"Failed to fetch PaymentTerms."));
            }
        }
        [HttpGet("{paymentTerms_Id}")]
        public async Task<IActionResult> Get(Guid paymentTerms_Id)
        {
            try
            {
                var paymentTermsList = await _paymentTermsService.GetPaymentTerms(paymentTerms_Id);
                return Ok(ApiResponse<object>.SuccessResponse(paymentTermsList, "PaymentTerms fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message,"Failed to fetch PaymentTerms."));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentTermsDTO payTerms)
        {
            try
            {
                var savedItem = await _paymentTermsService.Save(payTerms);
                return Ok(ApiResponse<object>.SuccessResponse(savedItem, "PaymentTerms saved successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message,"Failed to save PaymentTerms."));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PaymentTermsDTO payTerms)
        {
            try
            {
                var updatedItem = await _paymentTermsService.Update(payTerms);
                return Ok(ApiResponse<object>.SuccessResponse(updatedItem, "PaymentTerms Updated successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message,"Failed to update PaymentTerms."));
            }
        }
        [HttpDelete("{paymentTerms_Id}")]
        public async Task<IActionResult> Delete(Guid paymentTerms_Id)
        {
            try
            {
                var deleted = await _paymentTermsService.Delete(paymentTerms_Id);
                return Ok(ApiResponse<object>.SuccessResponse(deleted, "PaymentTerms Deleted successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message,"Failed to delete PaymentTerms."));
            }
        }
    }
}
