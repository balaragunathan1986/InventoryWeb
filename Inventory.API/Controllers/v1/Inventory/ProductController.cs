using Inventory.Domain.DTO.Inventory;
using Inventory.Domain.Model;
using Inventory.Services.Interfaces.Inventory;
using Inventory.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers.v1.Inventory
{
    [Route("api/v1/inventory/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var productList = await _productService.GetProduct();
                return Ok(ApiResponse<object>.SuccessResponse(productList, "Products fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to fetch products."));
            }
        }

        [HttpGet("{product_Id}")]
        public async Task<IActionResult> Get(Guid product_Id)
        {
            try
            {
                var product = await _productService.GetProduct(product_Id);
                return Ok(ApiResponse<object>.SuccessResponse(product, "Product fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to fetch product."));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDTO product)
        {
            try
            {
                var savedItem = await _productService.Save(product);
                return Ok(ApiResponse<object>.SuccessResponse(savedItem, "Product saved successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to save product."));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductDTO product)
        {
            try
            {
                var updatedItem = await _productService.Update(product);
                return Ok(ApiResponse<object>.SuccessResponse(updatedItem, "Product updated successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to update product."));
            }
        }

        [HttpDelete("{product_Id}")]
        public async Task<IActionResult> Delete(Guid product_Id)
        {
            try
            {
                var deleted = await _productService.Delete(product_Id);
                return Ok(ApiResponse<object>.SuccessResponse(deleted, "Product deleted successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to delete product."));
            }
        }
    }
}
