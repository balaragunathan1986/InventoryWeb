using Inventory.Domain.DTO.PointofSales;
using Inventory.Domain.Model;
using Inventory.Services.Interfaces.PointofSales;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers.v1.PointofSales;

[Route("api/v1/pointofsales/[controller]")]
[ApiController]
public class SalesReturnController : ControllerBase
{
    private readonly ISalesReturnService _service;

    public SalesReturnController(ISalesReturnService service)
    {
        _service = service;
    }

    [HttpGet("GenerateDocumentNumber")]
    public async Task<IActionResult> GenerateDocumentNumber() => await Execute(
        () => _service.GenerateDocumentNumber(), "Sales return number generated successfully.", "Failed to generate sales return number.");

    [HttpGet("ForSales/{salesId:guid}")]
    public async Task<IActionResult> GetSalesForReturn(Guid salesId) => await Execute(
        () => _service.GetSalesForReturn(salesId), "Sales fetched successfully.", "Failed to fetch sales for return.");

    [HttpGet]
    public async Task<IActionResult> Get() => await Execute(
        () => _service.GetSalesReturns(), "Sales returns fetched successfully.", "Failed to fetch sales returns.");

    [HttpGet("{salesReturnId:guid}")]
    public async Task<IActionResult> Get(Guid salesReturnId) => await Execute(
        () => _service.GetSalesReturn(salesReturnId), "Sales return fetched successfully.", "Failed to fetch sales return.");

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SalesReturnSaveRequest request) => await Execute(
        () => _service.Save(request), "Sales return saved successfully.", "Failed to save sales return.");

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] SalesReturnSaveRequest request) => await Execute(
        () => _service.Update(request), "Sales return updated successfully.", "Failed to update sales return.");

    [HttpDelete("{salesReturnId:guid}")]
    public async Task<IActionResult> Delete(Guid salesReturnId, [FromQuery] string? deletedBy) => await Execute(
        () => _service.Delete(salesReturnId, deletedBy), "Sales return deleted successfully.", "Failed to delete sales return.");

    private static async Task<IActionResult> Execute<T>(Func<Task<T>> operation, string successMessage, string errorMessage)
    {
        try
        {
            return new OkObjectResult(ApiResponse<object>.SuccessResponse(await operation(), successMessage));
        }
        catch (Exception exception)
        {
            return new BadRequestObjectResult(ApiResponse<string>.ErrorResponse(exception.Message, errorMessage));
        }
    }
}