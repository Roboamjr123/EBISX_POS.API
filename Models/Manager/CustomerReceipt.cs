using ManagerLibrary.Data.utils;
using ManagerLibrary.ManagerData;
using System.ComponentModel.DataAnnotations;

namespace EBISX_POS.API.Models.Manager
{
    public class CustomerReceipt
    {
        [Key]
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTimeOffset ReceiptDate { get; set; }
        public int OrderId { get; set; }
        public ReceiptType? ReceiptType { get; set; }

        // 🔹 Cashier & Branch
        public required User Cashier { get; set; }
        public int BranchId { get; set; }
        public StoreBranch Branch { get; set; } = null!;

        // 🔹 Payment Details
        public decimal GrossAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal CashReceived { get; set; }
        public decimal TotalPayments { get; set; }

        // 🔹 Printed Receipt Content
        public string? ReceiptContent { get; set; } = string.Empty;
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;
    }
}
