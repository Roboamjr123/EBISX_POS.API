using ManagerLibrary.ManagerData;
using System.ComponentModel.DataAnnotations;

namespace EBISX_POS.API.Models.Manager
{
    public class DailySalesSummary
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset ReportDate { get; set; } = DateTimeOffset.UtcNow;

        // 🔹 Total Sales Data
        public decimal TotalGrossSales { get; set; }
        public decimal TotalDiscounts { get; set; }
        public decimal TotalNetSales { get; set; }
        public decimal TotalPaymentsReceived { get; set; }

        // 🔹 Sales Breakdown
        public int TotalTransactions { get; set; }
        public int TotalVoidedTransactions { get; set; }
        public int TotalReturnedTransactions { get; set; }

        public int BranchId { get; set; }
        public StoreBranch Branch { get; set; } = null!;
    }
}
