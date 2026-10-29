using Inventory.Domain.DTO.PointofSales;
using Inventory.Domain.Model;
using Inventory.Services.Interfaces.PointofSales;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers.v1.PointofSales;

[Route("api/v1/pointofsales/[controller]")]
[ApiController]
public class CourierController : ControllerBase
{
    private readonly ICourierService _courierService;

    public CourierController(ICourierService courierService)
    {
        _courierService = courierService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(await _courierService.GetCouriers(), "Couriers fetched successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to fetch Couriers."));
        }
    }

    [HttpGet("{courierId:guid}")]
    public async Task<IActionResult> Get(Guid courierId)
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(await _courierService.GetCourier(courierId), "Courier fetched successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to fetch Courier."));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CourierSaveRequest request)
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(await _courierService.Save(request), "Courier saved successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to save Courier."));
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CourierSaveRequest request)
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(await _courierService.Update(request), "Courier updated successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to update Courier."));
        }
    }

    [HttpDelete("{courierId:guid}")]
    public async Task<IActionResult> Delete(Guid courierId)
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(await _courierService.Delete(courierId), "Courier deleted successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to delete Courier."));
        }
    }
}
