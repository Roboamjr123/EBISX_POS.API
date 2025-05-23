﻿using EBISX_POS.API.Data;
using EBISX_POS.API.Models;
using EBISX_POS.API.Services.DTO.Auth;
using EBISX_POS.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace EBISX_POS.API.Services.Repositories
{
    public class AuthRepository(DataContext _dataContext, IServiceProvider _services) : IAuth
    {
        public async Task<List<CashierDTO>> Cashiers()
        {
            var cashiers = await _dataContext.User
                .Where(u => u.UserRole == "Cashier" && u.IsActive)
                .Select(u => new CashierDTO
                {
                    Email = u.UserEmail,
                    Name = u.UserFName + " " + u.UserLName
                })
                .ToListAsync();

            return cashiers;
        }

        public async Task<(bool, string)> CashWithdrawDrawer(string cashierEmail, string managerEmail, decimal cash)
        {
            var timestamp = await _dataContext.Timestamp
                .Include(t => t.Cashier)
                .Include(t => t.ManagerLog)
                .FirstOrDefaultAsync(t => t.Cashier.UserEmail == cashierEmail && t.TsOut == null);

            var manager = await _dataContext.User
                .FirstOrDefaultAsync(m => m.UserEmail == managerEmail && m.UserRole != "Cashier" && m.IsActive);


            if (timestamp?.CashInDrawerAmount is not { } available)
                return (false, "No active session or drawer amount not set.");

            var tsIn = timestamp.TsIn;

            decimal totalCashInDrawer = await _dataContext.Order
                    .Where(o =>
                        o.Cashier.UserEmail == cashierEmail &&
                        !o.IsCancelled &&
                        !o.IsPending &&
                        !o.IsReturned &&
                        o.CreatedAt >= tsIn &&
                        o.CashTendered != null &&
                        o.TotalAmount != 0
                    )
                    .SumAsync(o =>
                        o.CashTendered!.Value - o.ChangeAmount!.Value
                    );

            if (cash > available + totalCashInDrawer)
                return (false, "Withdrawal exceeds drawer balance.");

            if (manager is null)
                return (false, "Invalid manager credentials.");

            timestamp.ManagerLog.Add(new UserLog
            {
                Manager = manager,
                WithdrawAmount = cash,
                Timestamp = timestamp,
                Action = "Cash Withdrawal"
            });

            await _dataContext.SaveChangesAsync();
            return (true, "Cash withdrawal recorded.");
        }

        public async Task<bool> ChangeMode(string managerEmail)
        {
            var manager = await _dataContext.User
                .FirstOrDefaultAsync(u => u.UserEmail == managerEmail && u.UserRole != "Cashier" && u.IsActive);

            if (manager == null)
                return false;

            var posTerminalInfo = await _dataContext.PosTerminalInfo.FirstOrDefaultAsync();
            if (posTerminalInfo == null)
            {
                throw new InvalidOperationException("POS Terminal Info not found");
            }

            // Toggle the training mode
            posTerminalInfo.IsTrainMode = !posTerminalInfo.IsTrainMode;

            // Log the mode change
            _dataContext.UserLog.Add(new UserLog
            {
                Manager = manager,
                Action = $"Changed to {(posTerminalInfo.IsTrainMode ? "Training" : "Live")} Mode"
            });

            await _dataContext.SaveChangesAsync();
            return posTerminalInfo.IsTrainMode;
        }

        public async Task<(bool, string)> CheckData()
        {
            var hasUsers = await _dataContext.User
                .AnyAsync(a => a.IsActive);
            var hasMenu = await _dataContext.Menu
                .AnyAsync();

            if (hasUsers || hasMenu)
            {
                // Already has at least one user or one menu item → no need to seed
                return (false, "Data is already set up; skipping seed.");
            }

            // No users AND no menus → safe to seed
            return (true, "No existing data found; ready to seed initial data.");
        }

        public async Task<(bool, string, string)> HasPendingOrder()
        {
            // Check if there's a pending order with a related cashier.
            var pendingOrder = await _dataContext.Order
                .Include(o => o.Cashier)
                .Where(o => o.IsPending)
                .FirstOrDefaultAsync();

            // Check if there's an active timestamp (meaning the cashier hasn't timed out yet).
            var activeTimestamp = await _dataContext.Timestamp
                .Include(t => t.Cashier)
                .Where(t => t.TsOut == null)
                .FirstOrDefaultAsync();

            // If a pending order exists, use its cashier information.
            if (pendingOrder != null)
            {
                return (true,
                        pendingOrder.Cashier.UserEmail,
                        $"{pendingOrder.Cashier.UserFName} {pendingOrder.Cashier.UserLName}");
            }
            // If no pending order but the cashier is still clocked in, return its information.
            else if (activeTimestamp != null)
            {
                return (true,
                        activeTimestamp.Cashier.UserEmail,
                        $"{activeTimestamp.Cashier.UserFName} {activeTimestamp.Cashier.UserLName}");
            }

            // If neither condition is met, return false with empty values.
            return (false, "", "");
        }

        public async Task<bool> IsCashedDrawer(string cashierEmail)
        {

            var timestamp = await _dataContext.Timestamp
                .Include(t => t.Cashier)
                .Where(t => t.Cashier.UserEmail == cashierEmail && t.TsOut == null && t.CashInDrawerAmount != null && t.CashInDrawerAmount >= 1000)
                .FirstOrDefaultAsync();

            return timestamp != null;
        }

        public async Task<bool> IsTrainMode()
        {
            var posTerminalInfo = await _dataContext.PosTerminalInfo.FirstOrDefaultAsync();
            if (posTerminalInfo == null)
            {
                throw new InvalidOperationException("POS Terminal Info not found");
            }
            return posTerminalInfo.IsTrainMode;
        }

        public async Task<(bool, string)> LoadData()
        {
            try
            {
                // Perform the actual seeding
                await SeedData.InitializeAsync(_services);
                return (true, "Seed data loaded successfully.");
            }
            catch (Exception ex)
            {
                // You could log ex here
                return (false, $"Seeding failed: {ex.Message}");
            }
        }

        public async Task<(bool success, bool isManager, string email, string name)> LogIn(LogInDTO dto)
        {
            // Try to look up a manager if one was supplied
            User? manager = null;
            if (!string.IsNullOrWhiteSpace(dto.ManagerEmail))
            {
                manager = await _dataContext.User
                    .FirstOrDefaultAsync(u =>
                        u.UserEmail == dto.ManagerEmail &&
                        u.UserRole != "Cashier" &&
                        u.IsActive);
            }

            // Try to look up a cashier if one was supplied
            User? cashier = null;
            if (!string.IsNullOrWhiteSpace(dto.CashierEmail))
            {
                cashier = await _dataContext.User
                    .FirstOrDefaultAsync(u =>
                        u.UserEmail == dto.CashierEmail &&
                        u.UserRole == "Cashier" &&
                        u.IsActive);
            }

            // If neither found, fail
            if (manager == null && cashier == null)
                return (false, false, "", "");

            // --- Manager-only login (no cashierEmail supplied) ---
            if (cashier == null)
            {
                // Log the manager login
                _dataContext.UserLog.Add(new UserLog
                {
                    Manager = manager!,          // we know manager != null here
                    Action = "Manager Login",
                });

                await _dataContext.SaveChangesAsync();

                return (
                    success: true,
                    isManager: true,
                    email: manager!.UserEmail,
                    name: $"{manager!.UserFName} {manager!.UserLName}"
                );
            }

            // --- Cashier login (may have manager approval) ---
            var timestamp = new Timestamp
            {
                TsIn = DateTimeOffset.Now,
                Cashier = cashier,    // non-null
                ManagerIn = manager,      // may be null if self-approved
                IsTrainMode = await IsTrainMode()
            };
            _dataContext.Timestamp.Add(timestamp);

            // Optionally log the manager's approval
            if (manager != null)
            {
                _dataContext.UserLog.Add(new UserLog
                {
                    Manager = manager,
                    Timestamp = timestamp,
                    Action = "Approved Cashier Login"
                });
            }

            await _dataContext.SaveChangesAsync();

            return (
                success: true,
                isManager: false,
                email: cashier.UserEmail,
                name: $"{cashier.UserFName} {cashier.UserLName}"
            );
        }


        public async Task<(bool, string)> LogOut(LogInDTO logOutDTO)
        {
            var manager = await _dataContext.User
                .Where(m => m.UserEmail == logOutDTO.ManagerEmail && m.UserRole != "Cashier" && m.IsActive)
                .FirstOrDefaultAsync();

            var cashier = await _dataContext.User
                .Where(m => m.UserEmail == logOutDTO.CashierEmail && m.UserRole == "Cashier" && m.IsActive)
                .FirstOrDefaultAsync();

            if (manager == null)
                return (false, "Invalid Credential!");
            if (cashier == null)
                return (false, "Invalid Credential of Cashier!");


            var pendingOrder = await _dataContext.Order
                .Where(o => o.IsPending && o.Cashier == cashier)
                .AnyAsync();

            if (pendingOrder)
                return (false, "Cashier has pending order!");

            var timestamp = await _dataContext.Timestamp
                .Where(t => t.Cashier == cashier && t.TsOut == null)
                .FirstOrDefaultAsync();

            if (timestamp == null)
                return (false, "Cashier is not clocked in!");

            timestamp.TsOut = DateTimeOffset.UtcNow; // Set the time-out to now
            timestamp.ManagerOut = manager; // Manager who authorized the logout

            await _dataContext.SaveChangesAsync();

            return (true, "Cashier Logged Out!");
        }

        public async Task<(bool, string)> SetCashInDrawer(string cashierEmail, decimal cash)
        {
            var timestamp = await _dataContext.Timestamp
                .Include(t => t.Cashier)
                .Where(t => t.Cashier.UserEmail == cashierEmail && t.TsOut == null)
                .FirstAsync();

            timestamp.CashInDrawerAmount = cash;

            if (timestamp.CashInDrawerAmount == null)
                return (false, "Cash in drawer amount is null!");

            await _dataContext.SaveChangesAsync();

            return (true, "Cash in drawer set!");
        }

        public async Task<(bool, string)> SetCashOutDrawer(string cashierEmail, decimal cash)
        {
            var timestamp = await _dataContext.Timestamp
                .Include(t => t.Cashier)
                .Where(t => t.Cashier.UserEmail == cashierEmail && t.TsOut == null)
                .FirstAsync();

            timestamp.CashOutDrawerAmount = cash;

            if (timestamp.CashInDrawerAmount == null)
                return (false, "Cash in drawer amount is null!");

            await _dataContext.SaveChangesAsync();

            return (true, "Cash out drawer set!");
        }
    }
}
