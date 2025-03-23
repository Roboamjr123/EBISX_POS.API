using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLibrary.Services.DTO.BranchStore
{
    public class BranchDto
    {
        public int Id { get; set; }
        public required string BranchName { get; set; }
        public required string BranchAddress { get; set; }
        public required string VAtRegTin { get; set; }
        public required string MinNo { get; set; }
        public required string SerialNumber { get; set; }
    }
}
