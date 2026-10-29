using Inventory.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.Interfaces.Master
{
    public interface IProductGroupService
    {
        Task<List<ProductGroupDTO>> GetProductGroup();
        Task<ProductGroupDTO> GetProductGroup(Guid ProductGroup_Id);
        Task<ProductGroupDTO> Save(ProductGroupDTO ProductGroup);
        Task<ProductGroupDTO> Update(ProductGroupDTO ProductGroup);
        Task<bool> Delete(Guid ProductGroup_Id);
    }
}
