﻿using FoodDelivery.BL.DTOs.Order;

namespace FoodDelivery.BL.Facades.OrderFacade;

public interface IOrderFacade
{
    public Task<OrderWithProductsGetDto?> GetByIdAsync(Guid id);

    public Task<IEnumerable<OrderWithProductsGetDto>> GetOrdersForUserAsync(string username);

    public Task<OrderWithProductsGetDto?> GetActiveOrderAsync(string username);

    public Task AddProductToCartAsync(string username, Guid productId);

    public Task FulfillOrderAsync(Guid orderId);

    public Task<MemoryStream> CreatePdfFromOrder(string url);
    
    public Task ApplyCouponCodeAsync(Guid orderId, string couponCode);
}
