using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    public class BillingReportDTO
    {
        public Guid Billing_Id { get; set; }

        // Company / Supplier
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyAddress { get; set; } = string.Empty;
        public string CompanyGSTIN { get; set; } = string.Empty;
        public string CompanyPhone { get; set; } = string.Empty;

        public string CompanyState { get; set; } = string.Empty;

        // Company / Supplier
        public string SupplierName { get; set; } = string.Empty;
        public string SupplierAddress { get; set; } = string.Empty;
        public string SupplierGSTIN { get; set; } = string.Empty;
        public string SupplierPhone { get; set; } = string.Empty;

        public string SupplierState { get; set; } = string.Empty;

        // Receipt
        public string ReceiptNumber { get; set; } = string.Empty;
        public DateTime ReceiptDate { get; set; }

        // Billing
        public string BillingNumber { get; set; } = string.Empty;
        public decimal BillingAmount { get; set; }

        public decimal SourceBillQty { get; set; }
        public string SourceBillUom { get; set; } = string.Empty;

        // Debit
        public List<BillingDebitDetailDTO> DebitDetails { get; set; }
            = new();

        public decimal TotalDebitQty { get; set; }
        public decimal NetQty { get; set; }
        public decimal NetAmount { get; set; }

        public string AmountInWords { get; set; } = string.Empty;

        public string Remarks { get; set; } = string.Empty;
    }

    public class BillingDebitDetailDTO
    {
        public int SlNo { get; set; }

        public string Deduction { get; set; } = string.Empty;

        public string HSNCode { get; set; } = string.Empty;

        public decimal Quantity { get; set; }

        public string Uom { get; set; } = string.Empty;

        public decimal Rate { get; set; }

        public decimal Amount { get; set; }
    }
}
