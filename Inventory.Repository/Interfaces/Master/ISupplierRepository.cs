using Inventory.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repository.Interfaces.Master
{
    public interface ISupplierRepository
    {
        Task<List<SupplierDTO>> GetSuppliers();
        Task<SupplierDTO> GetSupplier(Guid Company_Id);
        Task<SupplierDTO> Save(SupplierDTO Company);
        Task<SupplierDTO> Update(SupplierDTO Company);
        Task<bool> Delete(Guid Company_Id);
    }
}
