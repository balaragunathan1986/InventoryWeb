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
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<List<SupplierDTO>> GetSuppliers()
        {
            return await _supplierRepository.GetSuppliers();
        }
        public async Task<SupplierDTO> GetSupplier(Guid Supplier_Id)
        {
            return await _supplierRepository.GetSupplier(Supplier_Id);
        }
        public async Task<SupplierDTO> Save(SupplierDTO Supplier)
        {
            Supplier.Supplier_Id = Guid.NewGuid();
            return await _supplierRepository.Save(Supplier);
        }
        public async Task<SupplierDTO> Update(SupplierDTO Supplier)
        {
            return await _supplierRepository.Update(Supplier);
        }
        public async Task<bool> Delete(Guid Supplier_Id)
        {
            return await _supplierRepository.Delete(Supplier_Id);
        }

    }
}
