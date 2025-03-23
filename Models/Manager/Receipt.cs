using EBISX_POS.API.Models;
using ManagerLibrary.Data.utils;
using ManagerLibrary.ManagerData;
using ManagerLibrary.Services.Interface;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerLibrary.Data
{
    public class Receipt
    {
        // Primary Identifier
        [Key]
        public int Id { get; set; }

        // Customer & Transaction Details
        public string? ReceiptId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string InvoiceNumber { get; set; } = string.Empty;


        public DateTimeOffset ReceiptDate { get; set; }
        public DateTimeOffset DateIssued { get; set; }
        public DateTimeOffset ValidUntil { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        // Foreign Key for Order
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public ReceiptType ReceiptType { get; set; }


        // Report Metadata
        public DateTimeOffset ReportDate { get; set; }
        public DateTimeOffset ReportTime { get; set; } 
        public DateTimeOffset StartDateTime { get; set; } 
        public DateTimeOffset EndDateTime { get; set; }
        // Invoice Numbers
        public int BeginningSI { get; set; }
        public int EndingSI { get; set; }
        public int BeginningVOID { get; set; }
        public int EndingVOID { get; set; }
        public int BeginningRETURN { get; set; }
        public int EndingRETURN { get; set; }
        public int ResetCounter { get; set; }
        public int ZCounter { get; set; }

        // Sales Summary
        public decimal PresentAccumulatedSales { get; set; }
        public decimal PreviousAccumulatedSales { get; set; }
        public decimal SalesForTheDay { get; set; }

        // Breakdown of Sales
        public decimal VatableSales { get; set; }
        public decimal VatAmount { get; set; }
        public decimal VatExemptSales { get; set; }
        public decimal ZeroRatedSales { get; set; }

        // Net Sales Calculation
        public decimal GrossAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal ReturnAmount { get; set; }
        public decimal VoidAmount { get; set; }
        public decimal VatAdjustment { get; set; }
        public decimal NetAmount { get; set; }
        
        // Discount Summary
        public decimal SCDiscount { get; set; }
        public decimal PWDDiscount { get; set; }
        public decimal NAACDiscount { get; set; }
        public decimal SoloParentDiscount { get; set; }
        public decimal OtherDiscount { get; set; }

        // Sales Adjustments
        public decimal SalesVoid { get; set; }
        public decimal SalesReturn { get; set; }

        // VAT Adjustments
        public decimal SCTransaction { get; set; }
        public decimal PWDTransaction { get; set; }
        public decimal RegDiscTransaction { get; set; }
        public decimal ZeroRatedTransaction { get; set; }
        public decimal VatOnReturn { get; set; }
        public decimal OtherVatAdjustments { get; set; }

        // Payments Received
        public decimal CashReceived { get; set; }
        public decimal ChequeReceived { get; set; }
        public decimal CreditCardReceived { get; set; }
        public decimal GiftCertificate { get; set; }
        public decimal TotalPayments { get; set; }

        // Transaction Summary
        public decimal CashInDrawer { get; set; }
        public decimal OpeningFund { get; set; }
        public decimal WithdrawalAmount { get; set; }
        public decimal ShortOver { get; set; }

        // Transaction Log
        public string TransactionId { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public decimal TransactTotalAmount { get; set; }
        public string ReceiptContent { get; set; } = string.Empty;

        public required User Cashier { get; set; }
        public User? Manager { get; set; }

        // 🔹 Add Foreign Key for Branch
        public int BranchId { get; set; }  // Foreign key reference
        public StoreBranch Branch { get; set; } = null!;  // Navigation property

    }



}
