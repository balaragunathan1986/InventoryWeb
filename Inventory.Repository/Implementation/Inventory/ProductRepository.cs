using Inventory.Domain.DTO;
using Inventory.Domain.DTO.Inventory;
using Inventory.Repository.Data;
using Inventory.Repository.Interfaces.Inventory;
using Inventory.Repository.Interfaces.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repository.Implementation.Inventory
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDBContext _dbcontext;
        private readonly IConfiguration _configuration;
        public ProductRepository(AppDBContext _context, IConfiguration configuration)
        {
            _dbcontext = _context;
            _configuration = configuration;
        }

        public async Task<List<ProductDTO>> GetProduct()
        {
            return await _dbcontext.Product.Where(_ => !_.IsDeleted).ToListAsync();
        }

        public async Task<ProductDTO> GetProduct(Guid Product_Id)
        {


            var prd = await _dbcontext.Product.FirstOrDefaultAsync(item => item.Product_Id.ToString().ToLower() == Product_Id.ToString().ToLower());

            return prd ?? new ProductDTO();
        }

        public async Task<ProductDTO> Save(ProductDTO product)
        {
            await _dbcontext.Product.AddAsync(product);
            await _dbcontext.SaveChangesAsync();
            return product;
        }

        public async Task<ProductDTO> Update(ProductDTO product)
        {
            var pt = await _dbcontext.Product.FirstOrDefaultAsync(item => item.Product_Id == product.Product_Id && item.Id == product.Id && item.IsDeleted == false);
            if (pt != null)
            {
                pt.ProductName = product.ProductName;
                pt.ProductCode = product.ProductCode;
                pt.ProductDescription = product.ProductDescription;
                pt.ProductGroupId = product.ProductGroupId;
                pt.UomId = product.UomId;
                pt.ChargeId = product.ChargeId;
                pt.Barcode = product.Barcode;
                pt.HSNCode = product.HSNCode;
                pt.CostPrice = product.CostPrice;
                pt.SalesPrice = product.SalesPrice;
                pt.MRP = product.MRP;
                pt.StockQuantity = product.StockQuantity;
                pt.ManufactureDate = product.ManufactureDate;
                pt.ExpiryDate = product.ExpiryDate;
                pt.ImagePath = product.ImagePath;
                pt.Remarks = product.Remarks;
                pt.IsActive = product.IsActive;
                pt.UpdatedBy = product.UpdatedBy;
                pt.UpdatedAt = product.UpdatedAt;
                await _dbcontext.SaveChangesAsync();
                return pt;
            }
            return new ProductDTO();
        }
        public async Task<bool> Delete(Guid Product_Id)
        {
            var pt = await _dbcontext.Product.FirstOrDefaultAsync(item => item.Product_Id == Product_Id && item.IsDeleted == false);
            if (pt != null)
            {
                _dbcontext.Product.Remove(pt);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}
