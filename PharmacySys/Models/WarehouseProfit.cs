using System.ComponentModel.DataAnnotations;

namespace PharmacySys.Models
{
    public class WarehouseProfit
    {
        [Key]
        public string Warehouse { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalUnitCount { get; set; }
        public decimal AverageProfit { get; set; }
    }
}
