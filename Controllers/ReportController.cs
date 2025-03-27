
using ManagerLibrary.Services.DTO.BranchStore;
using ManagerLibrary.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EBISX_POS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReport _reportServices;
        public ReportController(IReport reportservices)
        {
            _reportServices = reportservices;
        }

        [HttpPost("add-branch")]
        public async Task<IActionResult> AddBranch([FromBody] BranchDto branchDto)
        {
            if (branchDto == null)
            {
                return BadRequest(new { message = "Invalid branch data" });
            }

            var result = await _reportServices.AddBranch(branchDto);
            return Ok(new { message = result });
        }

        [HttpGet("Branch-List")]
        public async Task<IActionResult> GetBranches()
        {
            var branches = await _reportServices.BranchList();

            if (branches == null || !branches.Any())
            {
                return NotFound(new { message = "No branches found" });
            }

            return Ok(branches);
        }

        [HttpGet("Customer-Receipts")]
        public async Task<IActionResult> GetCustomerReceipts()
        {
            var receipts = await _reportServices.GetCustomerReceipts();
            if (receipts == null || !receipts.Any())
            {
                return NotFound(new { message = "No receipts found" });
            }
            return Ok(receipts);
        }

        [HttpGet("Sales-Track")]
        public async Task<IActionResult> GetSalesTrack()
        {
            var salesTrack = await _reportServices.GetSalesTrack();
            if (salesTrack == null || !salesTrack.Any())
            {
                return NotFound(new { message = "No sales track found" });
            }
            return Ok(salesTrack);
        }

        [HttpGet("Daily-Sales-Receipts")]
        public async Task<IActionResult> GetDailySalesReceipts()
        {
            var dailySalesReceipts = await _reportServices.GetDailySalesReceipts();
            if (dailySalesReceipts == null || !dailySalesReceipts.Any())
            {
                return NotFound(new { message = "No daily sales receipts found" });
            }
            return Ok(dailySalesReceipts);
        }

    }
}
