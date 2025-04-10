﻿using EBISX_POS.API.Services.DTO.Auth;

namespace EBISX_POS.API.Services.Interfaces
{
    public interface IAuth
    {
        Task<(bool, string, string)> LogIn(LogInDTO logInDTO);
        Task<(bool, string)> LogOut(LogInDTO logOutDTO);
        Task<(bool, string, string)> HasPendingOrder();
        Task<List<CashierDTO>> Cashiers();


        Task<(bool, string)> SetCashInDrawer(string cashierEmail, decimal cash);
        Task<bool> IsCashedDrawer(string cashierEmail);
    }
}
