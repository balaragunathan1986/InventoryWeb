using Inventory.Domain.DTO.Inventory;
using Inventory.Domain.Model;
using Inventory.Services.Interfaces.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers.v1.Inventory
{
    [Route("api/v1/inventory/[controller]")]
    [ApiController]
    public class ProductStockController : ControllerBase
    {
        private readonly IProductStockService _productStockService;

        public ProductStockController(IProductStockService productStockService)
        {
            _productStockService = productStockService;
        }

        [HttpGet("GenerateDocumentNumber")]
        public async Task<IActionResult> GenerateDocumentNumber()
        {
            try
            {
                var number = await _productStockService.GenerateDocumentNumber();
                return Ok(ApiResponse<object>.SuccessResponse(number, "Billing Number generated successfully..!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(ex.Message, "Failed to generate billing number."));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var productStockList = await _productStockService.GetProductStock();
                return Ok(ApiResponse<object>.SuccessResponse(productStockList, "Product stocks fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to fetch product stocks."));
            }
        }

        [HttpGet("{productStock_Id}")]
        public async Task<IActionResult> Get(Guid productStock_Id)
        {
            try
            {
                var productStock = await _productStockService.GetProductStock(productStock_Id);
                return Ok(ApiResponse<object>.SuccessResponse(productStock, "Product stock fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to fetch product stock."));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductStockDTO productStock)
        {
            try
            {
                var savedItem = await _productStockService.Save(productStock);
                return Ok(ApiResponse<object>.SuccessResponse(savedItem, "Product stock saved successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to save product stock."));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductStockDTO productStock)
        {
            try
            {
                var updatedItem = await _productStockService.Update(productStock);
                return Ok(ApiResponse<object>.SuccessResponse(updatedItem, "Product stock updated successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to update product stock."));
            }
        }

        [HttpDelete("{productStock_Id}")]
        public async Task<IActionResult> Delete(Guid productStock_Id)
        {
            try
            {
                var deleted = await _productStockService.Delete(productStock_Id);
                return Ok(ApiResponse<object>.SuccessResponse(deleted, "Product stock deleted successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to delete product stock."));
            }
        }
    }
}
