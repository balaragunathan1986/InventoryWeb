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
    public class UomController : ControllerBase
    {
        private readonly IUomService _uomService;

        public UomController(IUomService uomService) {
            _uomService = uomService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try {
                var uomList = await _uomService.GetUom();
                return Ok(ApiResponse<object>.SuccessResponse(uomList, "Uom are fetched successfully..!"));
            }
            catch (Exception e) {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to fetch uom."));
            }
        }
        [HttpGet("{uom_Id}")]
        public async Task<IActionResult> Get(Guid uom_Id)
        {
            try
            {
                var uomList = await _uomService.GetUom(uom_Id);
                return Ok(ApiResponse<object>.SuccessResponse(uomList, "Uom fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to fetch uom."));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UomDTO uom)
        {
            try
            {
                var savedItem = await _uomService.Save(uom);
                return Ok(ApiResponse<object>.SuccessResponse(savedItem, "Uom saved successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to save uom."));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UomDTO uom)
        {
            try
            {
                var updatedItem = await _uomService.Update(uom);
                return Ok(ApiResponse<object>.SuccessResponse(updatedItem, "Uom Updated successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to update uom."));
            }
        }
        [HttpDelete("{uom_Id}")]
        public async Task<IActionResult> Delete(Guid uom_Id)
        {
            try
            {
                var deleted = await _uomService.Delete(uom_Id);
                return Ok(ApiResponse<object>.SuccessResponse(deleted, "Uom Deleted successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to delete uom."));
            }
        }
    }
}
