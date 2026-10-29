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
    public class ProductGroupRepository : IProductGroupRepository
    {
        private readonly AppDBContext _dbcontext;
        private readonly IConfiguration _configuration;
        public ProductGroupRepository(AppDBContext _context, IConfiguration configuration)
        {
            _dbcontext = _context;
            _configuration = configuration;
        }

        public async Task<List<ProductGroupDTO>> GetProductGroup()
        {
            return await _dbcontext.ProductGroup.Where(_ => !_.IsDeleted).ToListAsync();
        }

        public async Task<ProductGroupDTO> GetProductGroup(Guid ProductGroup_Id)
        {


            var paymode = await _dbcontext.ProductGroup.FirstOrDefaultAsync(item => item.ProductGroup_Id.ToString().ToLower() == ProductGroup_Id.ToString().ToLower());

            return paymode ?? new ProductGroupDTO();
        }

        public async Task<ProductGroupDTO> Save(ProductGroupDTO ProductGroup)
        {
            await _dbcontext.ProductGroup.AddAsync(ProductGroup);
            await _dbcontext.SaveChangesAsync();
            return ProductGroup;
        }

        public async Task<ProductGroupDTO> Update(ProductGroupDTO ProductGroup)
        {
            var productgroup = await _dbcontext.ProductGroup.FirstOrDefaultAsync(item => item.ProductGroup_Id == ProductGroup.ProductGroup_Id && item.Id == ProductGroup.Id && item.IsDeleted == false);
            if (productgroup != null)
            {
                productgroup.ProductGroupName = ProductGroup.ProductGroupName;
                productgroup.ProductGroupCode = ProductGroup.ProductGroupCode;
                productgroup.ProductGroupDescription = ProductGroup.ProductGroupDescription;
                productgroup.IsActive = ProductGroup.IsActive;
                productgroup.UpdatedBy = ProductGroup.UpdatedBy;
                productgroup.UpdatedAt = ProductGroup.UpdatedAt;
                await _dbcontext.SaveChangesAsync();
                return productgroup;
            }
            return new ProductGroupDTO();
        }
        public async Task<bool> Delete(Guid ProductGroup_Id)
        {
            var prodgrp = await _dbcontext.ProductGroup.FirstOrDefaultAsync(item => item.ProductGroup_Id == ProductGroup_Id && item.IsDeleted == false);
            if (prodgrp != null)
            {
                _dbcontext.ProductGroup.Remove(prodgrp);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}
