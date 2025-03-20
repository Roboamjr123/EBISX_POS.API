﻿using EBISX_POS.API.Services.DTO.Order;
using System.Collections.ObjectModel;

namespace EBISX_POS.API.Services.Interfaces
{
    public interface IOrder
    {
        Task<(bool, string)> AddOrderItem(AddOrderDTO addOrder);
        Task<(bool, string)> AddCurrentOrderVoid(AddCurrentOrderVoidDTO voidOrder);
        Task<(bool, string)> AddPwdScDiscount(AddPwdScDiscountDTO addPwdScDiscount);


        Task<(bool, string)> VoidOrderItem(VoidOrderItemDTO voidOrder);
        Task<(bool, string)> EditQtyOrderItem(EditOrderItemQuantityDTO editOrder);

        Task<(bool, string)> CancelCurrentOrder(string cashierEmail, string managerEmail);

        Task<List<GetCurrentOrderItemsDTO>> GetCurrentOrderItems(string cashierEmail);
    }
}
