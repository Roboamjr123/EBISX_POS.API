using ManagerLibrary.ManagerData;
using System.ComponentModel.DataAnnotations;

namespace EBISX_POS.API.Models.Manager
{
    public class OrderPurchaseSummary
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset ReportDate { get; set; } = DateTimeOffset.UtcNow;

        // 🔹 Order Details
        public int TotalOrders { get; set; }
        public decimal TotalOrderAmount { get; set; }
        public int TotalItemsSold { get; set; }

        public int BranchId { get; set; }
        public StoreBranch Branch { get; set; } = null!;
    }
}
