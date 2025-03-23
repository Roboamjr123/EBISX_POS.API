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
                    SerialNumber = addBranch.SerialNumber
                };
                _context.StoreBranches.Add(branch);
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
            var branches = await _context.StoreBranches.ToListAsync();

            return branches.Select(branch => new BranchDto
            {
                Id = branch.Id,
                BranchName = branch.BranchName,
                BranchAddress = branch.BranchAddress,
                VAtRegTin = branch.VAtRegTin,
                MinNo = branch.MinNo,
                SerialNumber = branch.SerialNumber
            }).ToList();
        }

        public Task<List<GetCustomerReceiptDto>> GetCustomerReceipts()
        {
            throw new NotImplementedException();
        }

        //    public  Task<List<GetCustomerReceiptDto>> GetCustomerReceipts()
        //    {
        //        try
        //        {

        //          var receipts = await _context.Receipts
        //         .Include(r => r.Order)
        //         .Include(r => r.Cashier)
        //         .Select(r => new GetCustomerReceiptDto
        //         {
        //             Id = r.Id,
        //             InvoiceNumber = r.InvoiceNumber,
        //             ReceiptDate = (DateTimeOffset)r.ReceiptDate, // No need for .HasValue or .Value
        //             OrderId = r.Order.Id,
        //             ReceiptType = r.ReceiptType,
        //             CashierName = r.Cashier.UserEmail,
        //             BranchId = r.Order.BranchId // Ensure this field exists in your Order entity
        //         }).ToListAsync();

        //        }
        //        catch (Exception ex)
        //        {
        //            return new List<GetCustomerReceiptDto>();
        //        }

        //    }
    
        }
    }
