using ManagerLibrary.Data.utils;
using ManagerLibrary.Services.DTO.BranchStore;

namespace EBISX_POS.API.Services.DTO.Manager
{
    public class GetCustomerReceiptDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } 
        public DateTimeOffset ReceiptDate { get; set; }
        public int OrderId { get; set; }
        public ReceiptType ReceiptType { get; set; }
        public string CashierName { get; set; }
        public int BranchId { get; set; }   

    }
}
