using Inventory.Domain.DTO.PointofSales;
using Inventory.Domain.Model;
using Inventory.Services.Interfaces.PointofSales;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers.v1.PointofSales;

[Route("api/v1/pointofsales/[controller]")]
[ApiController]
public class SalesController : ControllerBase
{
    private readonly ISalesService _salesService;

    public SalesController(ISalesService salesService)
    {
        _salesService = salesService;
    }

    [HttpGet("GenerateDocumentNumber")]
    public async Task<IActionResult> GenerateDocumentNumber()
    {
        try
        {
            var number = await _salesService.GenerateDocumentNumber();
            return Ok(ApiResponse<object>.SuccessResponse(number, "Sales number generated successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to generate sales number."));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(await _salesService.GetSales(), "Sales fetched successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to fetch Sales."));
        }
    }

    [HttpGet("{salesId:guid}")]
    public async Task<IActionResult> Get(Guid salesId)
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(await _salesService.GetSales(salesId), "Sales fetched successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to fetch Sales."));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SalesSaveRequest sales)
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(await _salesService.Save(sales), "Sales saved successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to save Sales."));
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] SalesSaveRequest sales)
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(await _salesService.Update(sales), "Sales updated successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to update Sales."));
        }
    }

    [HttpDelete("{salesId:guid}")]
    public async Task<IActionResult> Delete(Guid salesId)
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(await _salesService.Delete(salesId), "Sales deleted successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to delete Sales."));
        }
    }
}
