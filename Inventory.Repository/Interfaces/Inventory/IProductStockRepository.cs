using Inventory.Domain.DTO.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Repository.Interfaces.Inventory
{
    public interface IProductStockRepository
    {
        Task<List<ProductStockDTO>> GetProductStock();
        Task<ProductStockDTO> GetProductStock(Guid ProductStock_Id);
        Task<ProductStockDTO> Save(ProductStockDTO productStock);
        Task<ProductStockDTO> Update(ProductStockDTO productStock);
        Task<bool> Delete(Guid ProductStock_Id);

        Task<string> GenerateNumber();
    }
}
