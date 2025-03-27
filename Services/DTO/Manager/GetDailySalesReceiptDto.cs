namespace EBISX_POS.API.Services.DTO.Manager
{
    public class GetDailySalesReceiptDto
    {
        public string ReceiptDate { get; set; } = string.Empty;
        public string ReceiptTime { get; set; } = string.Empty;
        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? AmountDue { get; set; }
        public decimal GrossSales { get; set; }
        public decimal? Returns { get; set; }
        public decimal NetReturns => GrossSales - (Returns ?? 0m);
        public decimal LessPriceDiscount { get; set; }
        public decimal NetSales => (VatableSales ?? 0m) + (ExepmtSales ?? 0m);
        public decimal? VatableSales { get; set; }
        public decimal? ExepmtSales { get; set; }

        // Computed properties
        public decimal? VatAmount => VatableSales.HasValue ? VatableSales.Value * 0.12m : 0m;
        public decimal VatExclusiveSales => (VatableSales ?? 0m) / 1.12m;
        public decimal NetPayableAmount => NetSales - (Returns ?? 0m) - LessPriceDiscount;
         
        public decimal ChangeDue(decimal paymentReceived) => paymentReceived - (AmountDue ?? 0m);

    }
}
