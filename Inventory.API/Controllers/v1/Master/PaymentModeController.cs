using Inventory.Domain.DTO;
using Inventory.Domain.Model;
using Inventory.Services.Interfaces.Master;
using Inventory.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers.v1.Master
{
    [Route("api/v1/masters/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentModeController : ControllerBase
    {
        private readonly IPaymentModeService _paymentModeService;

        public PaymentModeController(IPaymentModeService payModeService)
        {
            _paymentModeService = payModeService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var paymentModeList = await _paymentModeService.GetPaymentMode();
                return Ok(ApiResponse<object>.SuccessResponse(paymentModeList, "PaymentMode are fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to fetch payment mode."));
            }
        }
        [HttpGet("{paymentMode_Id}")]
        public async Task<IActionResult> Get(Guid paymentMode_Id)
        {
            try
            {
                var paymentModeList = await _paymentModeService.GetPaymentMode(paymentMode_Id);
                return Ok(ApiResponse<object>.SuccessResponse(paymentModeList, "PaymentMode fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to fetch payment mode."));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentModeDTO payMode)
        {
            try
            {
                var savedItem = await _paymentModeService.Save(payMode);
                return Ok(ApiResponse<object>.SuccessResponse(savedItem, "PaymentMode saved successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to save payment mode."));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PaymentModeDTO payMode)
        {
            try
            {
                var updatedItem = await _paymentModeService.Update(payMode);
                return Ok(ApiResponse<object>.SuccessResponse(updatedItem, "PaymentMode Updated successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to update payment mode."));
            }
        }
        [HttpDelete("{paymentMode_Id}")]
        public async Task<IActionResult> Delete(Guid paymentMode_Id)
        {
            try
            {
                var deleted = await _paymentModeService.Delete(paymentMode_Id);
                return Ok(ApiResponse<object>.SuccessResponse(deleted, "PaymentMode Deleted successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to delete payment mode."));
            }
        }
    }
}
