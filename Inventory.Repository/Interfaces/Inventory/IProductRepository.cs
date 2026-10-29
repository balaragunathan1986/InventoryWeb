using Inventory.Domain.DTO;
using Inventory.Domain.DTO.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repository.Interfaces.Inventory
{
    public interface IProductRepository
    {
        Task<List<ProductDTO>> GetProduct();
        Task<ProductDTO> GetProduct(Guid Product_Id);
        Task<ProductDTO> Save(ProductDTO Product);
        Task<ProductDTO> Update(ProductDTO Product);
        Task<bool> Delete(Guid Product_Id);
    }
}
