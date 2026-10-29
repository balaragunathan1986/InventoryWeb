using Inventory.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repository.Interfaces.Master
{
    public interface IProductGroupRepository
    {
        Task<List<ProductGroupDTO>> GetProductGroup();
        Task<ProductGroupDTO> GetProductGroup(Guid ProductGroup_Id);
        Task<ProductGroupDTO> Save(ProductGroupDTO ProductGroup);
        Task<ProductGroupDTO> Update(ProductGroupDTO ProductGroup);
        Task<bool> Delete(Guid ProductGroup_Id);
    }
}
