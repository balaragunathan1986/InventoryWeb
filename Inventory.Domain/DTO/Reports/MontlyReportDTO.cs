namespace Inventory.Domain.DTO.Reports
{
    public class MontlyReportResponseDTO
    {
        public string FolderName { get; set; } = "Reports";
        public string ReportName { get; set; } = "MontlyReport";
        public List<MontlyReportDTO> Rows { get; set; } = new();
    }

    public class MontlyReportDTO
    {
        public DateTime? Date { get; set; }

        public string BatchNo { get; set; } = string.Empty;

        public decimal RawMaterialQty { get; set; }

        public decimal ProductionQty { get; set; }

        public decimal WastageQty { get; set; }

        public decimal YieldPercentage { get; set; }

        public decimal MaterialCost { get; set; }

        public decimal OtherCost { get; set; }

        public decimal TotalCost { get; set; }

        public decimal CostPerKg { get; set; }

        public decimal SalesAmount { get; set; }

        public decimal ProfitLoss { get; set; }

        public decimal ProfitPercentage { get; set; }

        public bool IsTotal { get; set; }
    }
}
