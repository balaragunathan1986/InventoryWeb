using Inventory.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.Interfaces.Master
{
    public interface ISupplierService
    {
        Task<List<SupplierDTO>> GetSuppliers();
        Task<SupplierDTO> GetSupplier(Guid Supplier_Id);
        Task<SupplierDTO> Save(SupplierDTO supplier);
        Task<SupplierDTO> Update(SupplierDTO supplier);
        Task<bool> Delete(Guid Supplier_Id);
    }
}
