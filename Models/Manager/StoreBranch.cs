using EBISX_POS.API.Models.Manager;
using ManagerLibrary.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLibrary.ManagerData
{
   public class StoreBranch
    {
        [Key]
        public int BranchId { get; set; }
        public required string BranchName { get; set; }
        public required string BranchAddress { get; set; }
        public required string VAtRegTin { get; set; }
        public required string MinNo { get; set; }  
        public required string SerialNumber { get; set; } 

        public required string PosNumber { get; set; }
        public DateTimeOffset ReportDate { get; set; } = DateTimeOffset.Now;
        public TimeSpan Time { get; set; } = DateTimeOffset.Now.TimeOfDay;

        // 🔹 One Branch Has Many Receipts
        public ICollection<Invoice> CustomerReceipts { get; set; } = new List<Invoice>();
    }
}
