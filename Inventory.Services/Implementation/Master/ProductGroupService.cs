using Inventory.Domain.DTO;
using Inventory.Repository.Interfaces.Master;
using Inventory.Services.Interfaces.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.Implementation.Master
{
    public class ProductGroupService : IProductGroupService
    {
        private readonly IProductGroupRepository _productGroupRepository;

        public ProductGroupService(IProductGroupRepository productGroupRepository)
        {
            _productGroupRepository = productGroupRepository;
        }

        public async Task<List<ProductGroupDTO>> GetProductGroup()
        {
            return await _productGroupRepository.GetProductGroup();
        }

        public async Task<ProductGroupDTO> GetProductGroup(Guid ProductGroup_Id)
        {
            return await _productGroupRepository.GetProductGroup(ProductGroup_Id);
        }

        public async Task<ProductGroupDTO> Save(ProductGroupDTO ProductGroup)
        {
            return await _productGroupRepository.Save(ProductGroup);
        }

        public async Task<ProductGroupDTO> Update(ProductGroupDTO ProductGroup)
        {
            return await _productGroupRepository.Update(ProductGroup);
        }

        public async Task<bool> Delete(Guid ProductGroup_Id)
        {
            return await _productGroupRepository.Delete(ProductGroup_Id);
        }
    }
}
