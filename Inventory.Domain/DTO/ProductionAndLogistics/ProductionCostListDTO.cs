using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.DTO.ProductionAndLogistics
{
    public class ProductionCostListDTO
    {
        public int Id { get; set; }
        public Guid Production_Cost_Id { get; set; }
        public string Production_Cost_Number { get; set; } = string.Empty;
        public string Production_Batch_Number { get; set; } = string.Empty;
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public decimal Total_Expenses { get; set; }     
    }
}
