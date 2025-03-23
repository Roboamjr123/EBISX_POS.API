using EBISX_POS.API.Services.DTO.Manager;
using ManagerLibrary.Services.DTO.BranchStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLibrary.Services.Interface
{
    public interface IReport
    {
        Task<string> AddBranch(BranchDto Addbranch);
        Task<List<BranchDto>> BranchList();
        Task<List<GetCustomerReceiptDto>> GetCustomerReceipts();
    }
}
