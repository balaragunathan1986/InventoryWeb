using Inventory.Domain.DTO;
using Inventory.Domain.Model;
using Inventory.Services.Interfaces.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers.v1.Master
{
    [Route("api/v1/masters/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var supplierList = await _supplierService.GetSuppliers();
                return Ok(ApiResponse<object>.SuccessResponse(supplierList, "Suppliers are fetched successfully..!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(ex.Message, "Failed to fetch suppliers."));
            }

        }

        [HttpGet("{Supplier_Id}")]
        public async Task<IActionResult> Get(Guid Supplier_Id)
        {
            try
            {
                var supplier = await _supplierService.GetSupplier(Supplier_Id);
                return Ok(ApiResponse<object>.SuccessResponse(supplier, "Supplier is fetched successfully..!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(ex.Message, "Failed to fetch company."));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SupplierDTO Supplier)
        {
            try
            {
                var supplier = await _supplierService.Save(Supplier);
                return Ok(ApiResponse<object>.SuccessResponse(supplier, "Supplier is saved successfully..!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(ex.Message, "Failed to save supplier."));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SupplierDTO Supplier)
        {
            try
            {
                var supplier = await _supplierService.Update(Supplier);
                return Ok(ApiResponse<object>.SuccessResponse(supplier, "Supplier is updated successfully..!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(ex.Message, "Failed to update supplier."));
            }
        }

        [HttpDelete("{Supplier_Id}")]
        public async Task<IActionResult> Delete(Guid Supplier_Id)
        {
            try
            {
                await _supplierService.Delete(Supplier_Id);
                return Ok(ApiResponse<string>.SuccessResponse("", "Supplier is deleted successfully..!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(ex.Message, "Failed to delete supplier."));
            }
        }
    }
}
