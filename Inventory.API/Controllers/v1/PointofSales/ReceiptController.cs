using Inventory.Domain.DTO.PointofSales;
using Inventory.Domain.Model;
using Inventory.Services.Interfaces.PointofSales;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers.v1.PointofSales;

[Route("api/v1/pointofsales/[controller]")]
[ApiController]
public class ReceiptController : ControllerBase
{
    private readonly IReceiptService _receiptService;

    public ReceiptController(IReceiptService receiptService)
    {
        _receiptService = receiptService;
    }

    [HttpGet("GenerateDocumentNumber")]
    public async Task<IActionResult> GenerateDocumentNumber()
    {
        try
        {
            var number = await _receiptService.GenerateDocumentNumber();
            return Ok(ApiResponse<object>.SuccessResponse(number, "Receipt number generated successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to generate receipt number."));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(await _receiptService.GetReceipts(), "Receipts fetched successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to fetch receipts."));
        }
    }

    [HttpGet("{receiptId:guid}")]
    public async Task<IActionResult> Get(Guid receiptId)
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(await _receiptService.GetReceipt(receiptId), "Receipt fetched successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to fetch receipt."));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ReceiptSaveRequest receipt)
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(await _receiptService.Save(receipt), "Receipt saved successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to save receipt."));
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] ReceiptSaveRequest receipt)
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(await _receiptService.Update(receipt), "Receipt updated successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to update receipt."));
        }
    }

    [HttpDelete("{receiptId:guid}")]
    public async Task<IActionResult> Delete(Guid receiptId)
    {
        try
        {
            return Ok(ApiResponse<object>.SuccessResponse(await _receiptService.Delete(receiptId), "Receipt deleted successfully..!"));
        }
        catch (Exception exception)
        {
            return BadRequest(ApiResponse<string>.ErrorResponse(exception.Message, "Failed to delete receipt."));
        }
    }
}