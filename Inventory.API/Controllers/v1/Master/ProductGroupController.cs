using Inventory.Domain.DTO;
using Inventory.Domain.Model;
using Inventory.Services.Interfaces.Master;
using Inventory.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers.v1.Master
{
    [Route("api/v1/masters/[controller]")]
    [ApiController]
    public class ProductGroupController : ControllerBase
    {
        private readonly IProductGroupService _productGroupService;

        public ProductGroupController(IProductGroupService productGroupService)
        {
            _productGroupService = productGroupService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var productGroupList = await _productGroupService.GetProductGroup();
                return Ok(ApiResponse<object>.SuccessResponse(productGroupList, "ProductGroup are fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to fetch PaymentTerms."));
            }
        }
        [HttpGet("{productGroup_Id}")]
        public async Task<IActionResult> Get(Guid productGroup_Id)
        {
            try
            {
                var productGroupList = await _productGroupService.GetProductGroup(productGroup_Id);
                return Ok(ApiResponse<object>.SuccessResponse(productGroupList, "ProductGroup fetched successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to fetch SalesType."));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductGroupDTO productGroup)
        {
            try
            {
                var savedItem = await _productGroupService.Save(productGroup);
                return Ok(ApiResponse<object>.SuccessResponse(savedItem, "ProductGroup saved successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to save SalesType."));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductGroupDTO productGroup)
        {
            try
            {
                var updatedItem = await _productGroupService.Update(productGroup);
                return Ok(ApiResponse<object>.SuccessResponse(updatedItem, "ProductGroup Updated successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to update SalesType."));
            }
        }
        [HttpDelete("{productGroup_Id}")]
        public async Task<IActionResult> Delete(Guid productGroup_Id)
        {
            try
            {
                var deleted = await _productGroupService.Delete(productGroup_Id);
                return Ok(ApiResponse<object>.SuccessResponse(deleted, "ProductGroup Deleted successfully..!"));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(e.Message, "Failed to delete SalesType."));
            }
        }
    }
}
