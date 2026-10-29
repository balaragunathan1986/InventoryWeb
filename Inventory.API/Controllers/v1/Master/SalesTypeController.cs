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
    public class SalesTypeController : ControllerBase
    {
        private readonly ISalesTypeService _salesTypeService;

        public SalesTypeController(ISalesTypeService salesTypeService)
        {
            _salesTypeService = salesTypeService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var salesTypeList = await _salesTypeService.GetSalesType();
                return Ok(ApiResponse<object>.SuccessResponse(salesTypeList, "SalesType are fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to fetch PaymentTerms."));
            }
        }
        [HttpGet("{salesType_Id}")]
        public async Task<IActionResult> Get(Guid salesType_Id)
        {
            try
            {
                var salesTypeList = await _salesTypeService.GetSalesType(salesType_Id);
                return Ok(ApiResponse<object>.SuccessResponse(salesTypeList, "SalesType fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to fetch SalesType."));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SalesTypeDTO salesType)
        {
            try
            {
                var savedItem = await _salesTypeService.Save(salesType);
                return Ok(ApiResponse<object>.SuccessResponse(savedItem, "SalesType saved successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to save SalesType."));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SalesTypeDTO salesType)
        {
            try
            {
                var updatedItem = await _salesTypeService.Update(salesType);
                return Ok(ApiResponse<object>.SuccessResponse(updatedItem, "SalesType Updated successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to update SalesType."));
            }
        }
        [HttpDelete("{salesType_Id}")]
        public async Task<IActionResult> Delete(Guid salesType_Id)
        {
            try
            {
                var deleted = await _salesTypeService.Delete(salesType_Id);
                return Ok(ApiResponse<object>.SuccessResponse(deleted, "SalesType Deleted successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to delete SalesType."));
            }
        }
    }
}
