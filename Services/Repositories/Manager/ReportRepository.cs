using EBISX_POS.API.Data;
using EBISX_POS.API.Services.DTO.Manager;
using ManagerLibrary.ManagerData;
using ManagerLibrary.Services.DTO.BranchStore;
using ManagerLibrary.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ManagerLibrary.Services.Repositories
{
    public class ReportRepository : IReport
    {
        private readonly DataContext _context;

        public ReportRepository(DataContext context)
        {
            _context = context;
        }

        public Task<string> AddBranch(BranchDto addBranch)
        {
            try
            {
                // ✅ Map DTO to Entity
                var branch = new StoreBranch
                {
                    BranchName = addBranch.BranchName,
                    BranchAddress = addBranch.BranchAddress,
                    VAtRegTin = addBranch.VAtRegTin,
                    MinNo = addBranch.MinNo,
                    SerialNumber = addBranch.SerialNumber,
                    PosNumber = addBranch.PosNumber
                };
                _context.StoreBranch.Add(branch);
                _context.SaveChanges();

                return Task.FromResult("Branch Added Successfully");
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }

        public async Task<List<BranchDto>> BranchList()
        {
            var branches = await _context.StoreBranch.ToListAsync();

            return branches.Select(branch => new BranchDto
            {
                Id = branch.BranchId,
                BranchName = branch.BranchName,
                BranchAddress = branch.BranchAddress,
                VAtRegTin = branch.VAtRegTin,
                MinNo = branch.MinNo,
                SerialNumber = branch.SerialNumber,
                PosNumber = branch.PosNumber
            }).ToList();
        }

        public async Task<List<GetCustomerReceiptDto>> GetCustomerReceipts()
        {

            return await _context.Invoice
                    .Include(r => r.Order)
                    .Include(r => r.Cashier)
                    .Include(r => r.Order.Branch)
                    .Where(r => r.Order != null && r.Cashier != null && r.Order.Branch != null) // Ensure no null references
                    .Select(r => new GetCustomerReceiptDto
                    {
                        Id = r.Id,
                        InvoiceNumber = r.InvoiceNumber,
                        ReceiptDate = r.ReceiptDate.ToString("MM/dd/yyyy") ?? "N/A", // No need for casting
                        ReceiptTime = r.ReceiptTime.ToString("HH:mm:ss") ?? "N/A", // No need for casting
                        OrderId = r.Order.Id,
                        TotalAmount = r.Order.TotalAmount,
                        Vatablesales = r.VatableSales,
                        VatAmount = r.VatAmount,
                        VatExemptSales = r.VatExemptSales,
                        ReceiptType = r.ReceiptType,
                        CashierName = r.Cashier.UserFName,
                        BranchName = r.Order.Branch.BranchName,
                        BranchAddress = r.Order.Branch.BranchAddress

                    })
                    .ToListAsync();
        }
        public async Task<List<GetCustomerReceiptDto>> GetCustomerReceiptById(string invoiceNumber)
        {
            return await _context.Invoice
                    .Include(r => r.Order)
                    .Include(r => r.Cashier)
                    .Include(r => r.Order.Branch)
                    .Where(r => r.InvoiceNumber == invoiceNumber)
                    .Select(r => new GetCustomerReceiptDto
                    {
                        Id = r.Id,
                        InvoiceNumber = r.InvoiceNumber,
                        ReceiptDate = r.ReceiptDate.ToString("MM/dd/yyyy") ?? "N/A",
                        ReceiptTime = r.ReceiptTime.ToString("HH:mm:ss") ?? "N/A",
                        OrderId = r.Order.Id,
                        TotalAmount = r.Order.TotalAmount,
                        Vatablesales = r.VatableSales,
                        VatAmount = r.VatAmount,
                        VatExemptSales = r.VatExemptSales,
                        ReceiptType = r.ReceiptType,
                        CashierName = r.Cashier.UserFName,
                        BranchName = r.Order.Branch.BranchName,
                        BranchAddress = r.Order.Branch.BranchAddress
                    })
                    .ToListAsync();
        }

        public async Task<List<GetSalesTrackDto>> GetSalesTrack()
        {
            var totalSales = await _context.Order.SumAsync(o => o.TotalAmount);
            var latestInvoice = await _context.Invoice
                                             .OrderByDescending(r => r.Id)
                                             .Select(r => new
                                             {
                                                 r.CashInDrawer,
                                                 r.ReportDate,
                                                 r.ReportTime
                                             })
                                             .FirstOrDefaultAsync();

            return new List<GetSalesTrackDto>
            {
                new GetSalesTrackDto
                {
                    TotalSales = totalSales,
                    CashInDrawer = latestInvoice?.CashInDrawer ?? 0,
                    ReportDate = latestInvoice?.ReportDate.ToString("MM/dd/yyy") ?? "N/A",
                    ReportTime = latestInvoice?.ReportTime.ToString("HH:mm:ss") ?? "N/A"
                }
            };
        }

        public async Task<List<GetDailySalesReceiptDto>> GetDailySalesReceipts()
        {
            var dailySalesReceipts = await _context.Invoice
                    .Select(i => new GetDailySalesReceiptDto
                {
                    ReceiptDate = i.ReceiptDate.ToString("MM/dd/yyyy"),
                    ReceiptTime = i.ReceiptTime.ToString("HH:mm:ss"),
                    InvoiceNumber = i.InvoiceNumber,
                    TotalAmount = i.GrossAmount,
                    GrossSales = i.GrossAmount,
                    Returns = i.ReturnAmount,
                    LessPriceDiscount = i.Discount,
                    VatableSales = i.VatableSales,
                    ExepmtSales = i.VatExemptSales,
                    AmountDue = null
                   }).ToListAsync();

            return dailySalesReceipts;
        }

        
    }
}
