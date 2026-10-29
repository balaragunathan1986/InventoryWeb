using Inventory.Domain.DTO.Inventory;
using Inventory.Repository.Data;
using Inventory.Repository.Interfaces.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Repository.Implementation.Inventory
{
    public class ProductStockRepository : IProductStockRepository
    {
        private readonly AppDBContext _dbcontext;
        private readonly IConfiguration _configuration;
        public ProductStockRepository(AppDBContext _context, IConfiguration configuration)
        {
            _dbcontext = _context;
            _configuration = configuration;
        }

        public async Task<string> GenerateNumber()
        {
            var lastNumber = await _dbcontext.ProductStock
                .OrderByDescending(x => x.ProductStockNo)
                .Select(x => x.ProductStockNo)
                .FirstOrDefaultAsync();

            return Utility.CommonFunctions.GenerateDocumentNumber(Utility.Costants.ProductStock, Utility.CommonFunctions.GetTransactionNumber(lastNumber));
        }

        public async Task<List<ProductStockDTO>> GetProductStock()
        {
            var productStockList = await (from ps in _dbcontext.ProductStock
                                           where !ps.IsDeleted
                                           join p in _dbcontext.Product on ps.Product_Id equals p.Product_Id into pj
                                           from p in pj.DefaultIfEmpty()
                                           join u in _dbcontext.Uom on ps.Uom_Id equals u.Uom_Id into uj
                                           from u in uj.DefaultIfEmpty()
                                           join c in _dbcontext.Company on ps.Company_Id equals c.Id into cj
                                           from c in cj.DefaultIfEmpty()
                                           select new { ps, ProductName = p.ProductName, UomName = u.Uom, CompanyName = c.Company_Name }
                                          ).ToListAsync();

            return productStockList.Select(item =>
            {
                item.ps.ProductName = item.ProductName;
                item.ps.UomName = item.UomName;
                item.ps.CompanyName = item.CompanyName;
                item.ps.BalQty = item.ps.Qty - (item.ps.AllotedQty ?? 0);
                return item.ps;
            }).ToList();
        }

        public async Task<ProductStockDTO> GetProductStock(Guid ProductStock_Id)
        {
            var ps = await _dbcontext.ProductStock.FirstOrDefaultAsync(item => item.ProductStock_Id == ProductStock_Id && !item.IsDeleted);

            if (ps != null)
            {
                ps.BalQty = ps.Qty - (ps.AllotedQty ?? 0);
            }

            return ps ?? new ProductStockDTO();
        }

        public async Task<ProductStockDTO> Save(ProductStockDTO productStock)
        {
            productStock.BalQty = productStock.Qty - (productStock.AllotedQty ?? 0);
            await _dbcontext.ProductStock.AddAsync(productStock);
            await _dbcontext.SaveChangesAsync();
            return productStock;
        }

        public async Task<ProductStockDTO> Update(ProductStockDTO productStock)
        {
            var ps = await _dbcontext.ProductStock.FirstOrDefaultAsync(item => item.ProductStock_Id == productStock.ProductStock_Id && item.Id == productStock.Id && item.IsDeleted == false);
            if (ps != null)
            {
                ps.ProductStockNo = productStock.ProductStockNo;
                ps.ItemCode = productStock.ItemCode;
                ps.Qty = productStock.Qty;
                ps.Rate = productStock.Rate;
                ps.AllotedQty = productStock.AllotedQty;
                ps.BalQty = productStock.Qty - (productStock.AllotedQty ?? 0);
                ps.SQty = productStock.SQty;
                ps.ReturnQty = productStock.ReturnQty;
                ps.MarkupRate = productStock.MarkupRate;
                ps.Product_Id = productStock.Product_Id;
                ps.Uom_Id = productStock.Uom_Id;
                ps.Company_Id = productStock.Company_Id;
                ps.ProductStockDate = productStock.ProductStockDate;
                ps.Remarks = productStock.Remarks;
                ps.IsActive = productStock.IsActive;
                ps.UpdatedBy = productStock.UpdatedBy;
                ps.UpdatedAt = productStock.UpdatedAt;
                await _dbcontext.SaveChangesAsync();
                return ps;
            }
            return new ProductStockDTO();
        }

        public async Task<bool> Delete(Guid ProductStock_Id)
        {
            var ps = await _dbcontext.ProductStock.FirstOrDefaultAsync(item => item.ProductStock_Id == ProductStock_Id && item.IsDeleted == false);
            if (ps != null)
            {
                ps.IsDeleted = true;
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}
