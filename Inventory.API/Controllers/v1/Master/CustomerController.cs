using Inventory.Domain.DTO;
using Inventory.Domain.Model;
using Inventory.Services.Interfaces.Master;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers.v1.Master;

[Route("api/v1/masters/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(
                await _customerService.GetCustomers(),
                "Customers are fetched successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to fetch customers."));
        }
    }

    [HttpGet("{customerId:guid}")]
    public async Task<IActionResult> Get(Guid customerId)
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(
                await _customerService.GetCustomer(customerId),
                "Customer is fetched successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to fetch customer."));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CustomerDTO customer)
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(
                await _customerService.Save(customer),
                "Customer is saved successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to save customer."));
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CustomerDTO customer)
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(
                await _customerService.Update(customer),
                "Customer is updated successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to update customer."));
        }
    }

    [HttpDelete("{customerId:guid}")]
    public async Task<IActionResult> Delete(Guid customerId)
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(
                await _customerService.Delete(customerId),
                "Customer is deleted successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to delete customer."));
        }
    }
}
