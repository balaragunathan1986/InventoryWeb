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
    public class ChargeController : ControllerBase
    {
        private readonly IChargeService _chargeService;

        public ChargeController(IChargeService chargeService)
        {
            _chargeService = chargeService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var productGroupList = await _chargeService.GetCharge();
                return Ok(ApiResponse<object>.SuccessResponse(productGroupList, "Charge are fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to fetch PaymentTerms."));
            }
        }
        [HttpGet("{charge_Id}")]
        public async Task<IActionResult> Get(Guid charge_Id)
        {
            try
            {
                var productGroupList = await _chargeService.GetCharge(charge_Id);
                return Ok(ApiResponse<object>.SuccessResponse(productGroupList, "Charge fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to fetch SalesType."));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChargeDTO charge)
        {
            try
            {
                var savedItem = await _chargeService.Save(charge);
                return Ok(ApiResponse<object>.SuccessResponse(savedItem, "Charge saved successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to save SalesType."));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ChargeDTO charge)
        {
            try
            {
                var updatedItem = await _chargeService.Update(charge);
                return Ok(ApiResponse<object>.SuccessResponse(updatedItem, "Charge Updated successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to update SalesType."));
            }
        }
        [HttpDelete("{charge_Id}")]
        public async Task<IActionResult> Delete(Guid charge_Id)
        {
            try
            {
                var deleted = await _chargeService.Delete(charge_Id);
                return Ok(ApiResponse<object>.SuccessResponse(deleted, "Charge Deleted successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to delete SalesType."));
            }
        }
    }
}
