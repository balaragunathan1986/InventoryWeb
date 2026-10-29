namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    public class DashboardOverviewDTO
    {
        public int Open_Job_Receipts { get; set; }
        public int In_Production { get; set; }
        public int Ready_To_Despatch { get; set; }
        public int Pending_Billing { get; set; }
        public int Completed_Production_Batches { get; set; }
        public int Total_Production_Batches { get; set; }
        public int Despatched_Production_Batches { get; set; }
        public decimal Production_Readiness_Percentage { get; set; }
        public decimal Despatch_Readiness_Percentage { get; set; }
        public decimal Total_Production_Output { get; set; }
        public List<DashboardOutputDTO> Production_Output { get; set; } = new();
        public List<DashboardActivityDTO> Recent_Activities { get; set; } = new();
    }

    public class DashboardOutputDTO
    {
        public DateTime Date { get; set; }
        public decimal Quantity { get; set; }
    }

    public class DashboardActivityDTO
    {
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Reference { get; set; }
        public DateTime Occurred_At { get; set; }
    }
}