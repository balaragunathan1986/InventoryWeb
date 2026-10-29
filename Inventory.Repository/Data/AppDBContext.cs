using Inventory.Domain.DTO;
using Inventory.Domain.DTO.Inventory;
using Inventory.Domain.DTO.ProductionAndLogistics;
using Inventory.Domain.DTO.Reports;
using Inventory.Domain.DTO.PointofSales;
using Inventory.Domain.Model.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Inventory.Repository.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        // Map your entity to a database table
        //public DbSet<Product> Products { get; set; }
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserPermissions> UserPermissions { get; set; }
        public DbSet<ApplicationMenus> ApplicationMenus { get; set; }
        public DbSet<EmailSetup> EmailSetup { get; set; }
        public DbSet<VerifyCode> VerifyCode { get; set; }

        #region Masters
        public DbSet<UomDTO> Uom {  get; set; }
        public DbSet<DepartmentDTO> Department { get; set; }
        public DbSet<DesignationDTO> Designation { get; set; }
        public DbSet<EmployeeDTO> Employee { get; set; }
        public DbSet<PaymentModeDTO> PaymentMode { get; set; }
        public DbSet<PaymentTermsDTO> PaymentTerms { get; set; }
        public DbSet<CompanyDTO> Company { get; set; }
        public DbSet<BankDetailsDTO> BankDetails { get; set; }
        public DbSet<SalesTypeDTO> SalesType { get; set; }
        public DbSet<ProductGroupDTO> ProductGroup { get; set; }
        public DbSet<ChargeDTO> Charge { get; set; }
        public DbSet<SupplierDTO> Supplier { get; set; }
        public DbSet<CustomerDTO> Customer { get; set; }

        #endregion

        #region Production
        public DbSet<JobReceiptDTO> JobReceipt { get; set; }
        public DbSet<JobReceiptItemDTO> JobReceiptItems { get; set; }
        public DbSet<JobReceiptListDTO> JobReceiptList { get; set; }
        public DbSet<JobReceiptChargeDTO> JobReceiptCharge { get; set; }
        public DbSet<ProductionBatchDTO> ProductionBatch { get; set; }
        public DbSet<ProductionBatchDetailDTO> ProductionBatchDetails { get; set; }
        public DbSet<ProductionBatchListDTO> ProductionBatchList { get; set; }
        public DbSet<ProductionCostDTO> ProductionCost { get; set; }
        public DbSet<ProductionCostChargeDTO> ProductionCostCharges { get; set; }
        public DbSet<ProductionCostScreeningDTO> ProductionCostScreening { get; set; }
        public DbSet<ProductionCostListDTO> ProductionCostList { get; set; }
        public DbSet<DespatchDTO> Despatch { get; set; }
        public DbSet<PaymentVoucherDTO> PaymentVoucher { get; set; }
        public DbSet<BillingMasterDTO> BillingMaster { get; set; }
        public DbSet<BillingDetailDTO> BillingDetails { get; set; }
        public DbSet<BillingReceiptMasterDTO> BillingReceiptMaster { get; set; }
        public DbSet<BillingReceiptDetailDTO> BillingReceiptDetails { get; set; }

        public DbSet<WagesDTO> Wages { get; set; }
        public DbSet<WagesChargesDTO> WagesCharges { get; set; }

        public DbSet<MontlyReportDTO> MontlyReport { get; set; }
        #endregion

        #region Inventory
        public DbSet<ProductDTO> Product { get; set; }
        public DbSet<ProductStockDTO> ProductStock { get; set; }

        #region Point of Sales
        public DbSet<SalesDTO> Sales { get; set; }
        public DbSet<CourierDTO> Courier { get; set; }
        public DbSet<SalesDetailDTO> SalesDetails { get; set; }
        public DbSet<SalesChargeDTO> SalesCharges { get; set; }
        public DbSet<SalesStockAllocationDTO> SalesStockAllocations { get; set; }
        public DbSet<SalesReturnDTO> SalesReturns { get; set; }
        public DbSet<SalesReturnDetailDTO> SalesReturnDetails { get; set; }
        public DbSet<SalesReturnStockDTO> SalesReturnStocks { get; set; }
        public DbSet<ReceiptDTO> Receipts { get; set; }
        public DbSet<ReceiptAllocationDTO> ReceiptAllocations { get; set; }
        #endregion
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserDTO>();
            modelBuilder.Entity<Roles>();
            modelBuilder.Entity<EmailSetup>();
            modelBuilder.Entity<VerifyCode>();

            modelBuilder.Entity<UomDTO>();
            modelBuilder.Entity<DepartmentDTO>();
            modelBuilder.Entity<DesignationDTO>();
            modelBuilder.Entity<EmployeeDTO>();
            modelBuilder.Entity<PaymentModeDTO>();
            modelBuilder.Entity<CompanyDTO>();
            modelBuilder.Entity<BankDetailsDTO>();
            modelBuilder.Entity<SupplierDTO>();
            modelBuilder.Entity<CustomerDTO>();

            modelBuilder.Entity<ProductionBatchListDTO>().HasNoKey();
            modelBuilder.Entity<JobReceiptListDTO>().HasNoKey();
            // modelBuilder.Entity<ProductionCostListDTO>().HasNoKey();
            modelBuilder.Entity<MontlyReportDTO>()
      .HasNoKey();

            modelBuilder.Entity<ProductDTO>();
            modelBuilder.Entity<ProductStockDTO>();

            modelBuilder.Entity<SalesChargeDTO>()
                .Property(salesCharge => salesCharge.Charge_Type)
                .HasConversion<string>();

            modelBuilder.Entity<CourierDTO>()
                .Ignore(courier => courier.DeletedBy)
                .Ignore(courier => courier.DeletedAt);
        }
    }
}
