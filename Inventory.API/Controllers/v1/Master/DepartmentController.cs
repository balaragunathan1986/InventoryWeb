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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var departments = await _departmentService.GetDepartment();
                return Ok(ApiResponse<object>.SuccessResponse(departments, "Departments fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to fetch departments."));
            }
        }

        [HttpGet("{department_Id}")]
        public async Task<IActionResult> Get(Guid department_Id)
        {
            try
            {
                var department = await _departmentService.GetDepartment(department_Id);
                return Ok(ApiResponse<object>.SuccessResponse(department, "Department fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to fetch department."));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DepartmentDTO department)
        {
            try
            {
                var savedItem = await _departmentService.Save(department);
                return Ok(ApiResponse<object>.SuccessResponse(savedItem, "Department saved successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to save department."));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] DepartmentDTO department)
        {
            try
            {
                var updatedItem = await _departmentService.Update(department);
                return Ok(ApiResponse<object>.SuccessResponse(updatedItem, "Department updated successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to update department."));
            }
        }

        [HttpDelete("{department_Id}")]
        public async Task<IActionResult> Delete(Guid department_Id)
        {
            try
            {
                var deleted = await _departmentService.Delete(department_Id);
                return Ok(ApiResponse<object>.SuccessResponse(deleted, "Department deleted successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to delete department."));
            }
        }
    }
}
