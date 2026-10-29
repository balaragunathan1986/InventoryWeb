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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var employees = await _employeeService.GetEmployee();
                return Ok(ApiResponse<object>.SuccessResponse(employees, "Employees fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to fetch employees."));
            }
        }

        [HttpGet("{employee_Id}")]
        public async Task<IActionResult> Get(Guid employee_Id)
        {
            try
            {
                var employee = await _employeeService.GetEmployee(employee_Id);
                return Ok(ApiResponse<object>.SuccessResponse(employee, "Employee fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to fetch employee."));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeDTO employee)
        {
            try
            {
                var savedItem = await _employeeService.Save(employee);
                return Ok(ApiResponse<object>.SuccessResponse(savedItem, "Employee saved successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to save employee."));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EmployeeDTO employee)
        {
            try
            {
                var updatedItem = await _employeeService.Update(employee);
                return Ok(ApiResponse<object>.SuccessResponse(updatedItem, "Employee updated successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to update employee."));
            }
        }

        [HttpDelete("{employee_Id}")]
        public async Task<IActionResult> Delete(Guid employee_Id)
        {
            try
            {
                var deleted = await _employeeService.Delete(employee_Id);
                return Ok(ApiResponse<object>.SuccessResponse(deleted, "Employee deleted successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(e.Message, "Failed to delete employee."));
            }
        }
    }
}
