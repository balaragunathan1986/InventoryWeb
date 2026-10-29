using Inventory.Domain.DTO;
using Inventory.Domain.DTO.Inventory;
using Inventory.Repository.Interfaces.Inventory;
using Inventory.Repository.Interfaces.Master;
using Inventory.Services.Interfaces.Inventory;
using Inventory.Services.Interfaces.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.Implementation.Inventory
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductDTO>> GetProduct()
        {
            return await _productRepository.GetProduct();
        }

        public async Task<ProductDTO> GetProduct(Guid Product_Id)
        {
            return await _productRepository.GetProduct(Product_Id);
        }

        public async Task<ProductDTO> Save(ProductDTO Product)
        {
            return await _productRepository.Save(Product);
        }

        public async Task<ProductDTO> Update(ProductDTO Product)
        {
            return await _productRepository.Update(Product);
        }

        public async Task<bool> Delete(Guid Product_Id)
        {
            return await _productRepository.Delete(Product_Id);
        }
    }
}
