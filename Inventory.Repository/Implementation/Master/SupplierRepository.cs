using Inventory.Domain.DTO;
using Inventory.Repository.Data;
using Inventory.Repository.Interfaces.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repository.Implementation.Master
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AppDBContext _dbcontext;
        private readonly IConfiguration _configuration;
        public SupplierRepository(AppDBContext _context, IConfiguration configuration)
        {
            _dbcontext = _context;
            _configuration = configuration;
        }

        public async Task<List<SupplierDTO>> GetSuppliers()
        {
            return await _dbcontext.Supplier.Where(_ => !_.IsDeleted).ToListAsync();
        }

        public async Task<SupplierDTO> GetSupplier(Guid Supplier_Id)
        {
            return await _dbcontext.Supplier.FirstOrDefaultAsync(item => item.Supplier_Id.ToString().ToLower() == Supplier_Id.ToString().ToLower()) ?? new SupplierDTO();
        }

        public async Task<SupplierDTO> Save(SupplierDTO supplier)
        {
            _dbcontext.Supplier.Add(supplier);
            await _dbcontext.SaveChangesAsync();
            return supplier;
        }

        public async Task<SupplierDTO> Update(SupplierDTO supplier)
        {
            var supplierItem = await _dbcontext.Supplier.FirstOrDefaultAsync(item => item.Supplier_Id == supplier.Supplier_Id && item.Id == supplier.Id && item.IsDeleted == false);
            if (supplierItem != null)
            {
                supplierItem.Supplier_Name = supplier.Supplier_Name;
                supplierItem.Address1 = supplier.Address1;
                supplierItem.Address2 = supplier.Address2;
                supplierItem.City = supplier.City;
                supplierItem.Supplier_State = supplier.Supplier_State;
                supplierItem.Country = supplier.Country;
                supplierItem.Phone_Number = supplier.Phone_Number;
                supplierItem.Email = supplier.Email;
                supplierItem.Gst_Number = supplier.Gst_Number;
                supplierItem.IsActive = supplier.IsActive;
                supplierItem.UpdatedBy = supplier.UpdatedBy;
                supplierItem.UpdatedAt = supplier.UpdatedAt;
                await _dbcontext.SaveChangesAsync();
                return supplierItem;
            }
            return new SupplierDTO();

        }

        public async Task<bool> Delete(Guid Supplier_Id)
        {
            var supplier = await _dbcontext.Supplier.FirstOrDefaultAsync(item => item.Supplier_Id == Supplier_Id && item.IsDeleted == false);
            if (supplier != null)
            {
                _dbcontext.Supplier.Remove(supplier);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}
