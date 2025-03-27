using ManagerLibrary.Data.utils;
using System.Text.Json.Serialization;


namespace EBISX_POS.API.Services.DTO.Manager
{
    public class GetCustomerReceiptDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public string CashierName { get; set; }
        public string ReceiptDate { get; set; } = string.Empty;
        public string ReceiptTime { get; set; } = string.Empty;

        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }

        public decimal Vatablesales { get; set; }
        public decimal VatAmount { get; set; }
        public decimal VatExemptSales { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))] 
        public ReceiptType ReceiptType { get; set; }
        public string BranchName { get; set; }   
        public string BranchAddress { get; set; }
       

    }
}
