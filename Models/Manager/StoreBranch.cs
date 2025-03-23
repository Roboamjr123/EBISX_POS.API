using ManagerLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLibrary.ManagerData
{
   public class StoreBranch
    {
        public int Id { get; set; }
        public required string BranchName { get; set; }
        public required string BranchAddress { get; set; }
        public required string VAtRegTin { get; set; }
        public required string MinNo { get; set; }  
        public required string SerialNumber { get; set; }  
        public DateTimeOffset ReportDate { get; set; } = DateTimeOffset.Now;
        public TimeSpan Time { get; set; } = DateTimeOffset.Now.TimeOfDay;

        // 🔹 One Branch Has Many Receipts
        public ICollection<Receipt> CustomerReceipts { get; set; } = new List<Receipt>();
    }
}
