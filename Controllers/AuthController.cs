﻿using EBISX_POS.API.Services.DTO.Auth;
using EBISX_POS.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EBISX_POS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController(IAuth _auth) : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> Cashiers()
        {
            var cashiers = await _auth.Cashiers();
            return Ok(cashiers);
        }

        [HttpGet()]
        public async Task<IActionResult> IsCashedDrawer(string cashierEmail)
        {
            var isCashedDrawer = await _auth.IsCashedDrawer(cashierEmail);
            if (!isCashedDrawer)
                return BadRequest("Cash Drawer is not set!");
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> HasPendingOrder()
        {
            var (success, pendingCashierEmail, cashierName) = await _auth.HasPendingOrder();
            if (success)
            {

                return Ok(new { cashierEmail = pendingCashierEmail, cashierName });
            }

            return BadRequest("No Pending Orders");
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInDTO logInDTO)
        {
            var (success, cashierEmail, cashierName) = await _auth.LogIn(logInDTO);
            if (!success)
                return Unauthorized("Invalid Credential!");

            return Ok(new { cashierEmail, cashierName });
        }

        [HttpPut]
        public async Task<IActionResult> LogOut(string cashierEmail, string managerEmail)
        {

            var (success, message) = await _auth.LogOut(new LogInDTO() { CashierEmail = cashierEmail, ManagerEmail = managerEmail });
            if (!success)
                return BadRequest(message);

            return Ok(new { message });
        }

        [HttpPut]
        public async Task<IActionResult> SetCashInDrawer(string cashierEmail, decimal cash)
        {

            var (success, message) = await _auth.SetCashInDrawer(cashierEmail, cash);
            if (!success)
                return BadRequest(message);

            return Ok(new { message });
        }

        [HttpPut]
        public async Task<IActionResult> SetCashOutDrawer(string cashierEmail, decimal cash)
        {

            var (success, message) = await _auth.SetCashOutDrawer(cashierEmail, cash);
            if (!success)
                return BadRequest(message);

            return Ok(new { message });
        }
    }
}
