using Inventory.Domain.DTO;
using Inventory.Domain.Model;
using Inventory.Services.Interfaces.Master;
using Inventory.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers.v1.Master
{
    [Route("api/v1/masters/[controller]")]
    [ApiController]
    [Authorize]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationService _designationService;

        public DesignationController(IDesignationService designationService)
        {
            _designationService = designationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var designations = await _designationService.GetDesignation();
                return Ok(ApiResponse<object>.SuccessResponse(designations, "Designations fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to fetch designations."));
            }
        }

        [HttpGet("{designation_Id}")]
        public async Task<IActionResult> Get(Guid designation_Id)
        {
            try
            {
                var designation = await _designationService.GetDesignation(designation_Id);
                return Ok(ApiResponse<object>.SuccessResponse(designation, "Designation fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to fetch designation."));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DesignationDTO designation)
        {
            try
            {
                var savedItem = await _designationService.Save(designation);
                return Ok(ApiResponse<object>.SuccessResponse(savedItem, "Designation saved successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to save designation."));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] DesignationDTO designation)
        {
            try
            {
                var updatedItem = await _designationService.Update(designation);
                return Ok(ApiResponse<object>.SuccessResponse(updatedItem, "Designation updated successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to update designation."));
            }
        }

        [HttpDelete("{designation_Id}")]
        public async Task<IActionResult> Delete(Guid designation_Id)
        {
            try
            {
                var deleted = await _designationService.Delete(designation_Id);
                return Ok(ApiResponse<object>.SuccessResponse(deleted, "Designation deleted successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to delete designation."));
            }
        }
    }
}
