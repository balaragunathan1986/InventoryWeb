using Inventory.Domain.DTO.Inventory;
using Inventory.Repository.Interfaces.Inventory;
using Inventory.Services.Interfaces.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Services.Implementation.Inventory
{
    public class ProductStockService : IProductStockService
    {
        private readonly IProductStockRepository _productStockRepository;

        public ProductStockService(IProductStockRepository productStockRepository)
        {
            _productStockRepository = productStockRepository;
        }

        public async Task<string> GenerateDocumentNumber()
         => await _productStockRepository.GenerateNumber();
        public async Task<List<ProductStockDTO>> GetProductStock()
        {
            return await _productStockRepository.GetProductStock();
        }

        public async Task<ProductStockDTO> GetProductStock(Guid ProductStock_Id)
        {
            return await _productStockRepository.GetProductStock(ProductStock_Id);
        }

        public async Task<ProductStockDTO> Save(ProductStockDTO productStock)
        {
            return await _productStockRepository.Save(productStock);
        }

        public async Task<ProductStockDTO> Update(ProductStockDTO productStock)
        {
            return await _productStockRepository.Update(productStock);
        }

        public async Task<bool> Delete(Guid ProductStock_Id)
        {
            return await _productStockRepository.Delete(ProductStock_Id);
        }
    }
}
