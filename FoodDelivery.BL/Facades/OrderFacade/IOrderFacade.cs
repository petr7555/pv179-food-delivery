using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.DTOs.Rating;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Facades.OrderFacade;

public interface IOrderFacade
{
    public Task<OrderWithProductsGetDto?> GetByIdAsync(Guid id);

    public Task<IEnumerable<OrderWithProductsGetDto>> GetOrdersForUserAsync(string username);

    public Task<OrderWithProductsGetDto?> GetActiveOrderAsync(string username);

    public Task AddProductToCartAsync(string username, Guid productId);

    public Task MarkOrderAsPaid(Guid orderId);

    public Task SubmitOrderAsync(Guid orderId);

    public Task<MemoryStream> CreatePdfFromOrder(string url);

    public Task ApplyCouponCodeAsync(Guid orderId, string couponCode);

    public Task<string> PayByCardAsync(OrderWithProductsGetDto order, string domain);

    public Task SetPaymentMethodAsync(Guid orderId, PaymentMethod paymentMethod);
    
    public Task DeleteProductFromOrderAsync(OrderWithProductsGetDto order, Guid productId);
    
    public Task DeleteCouponFromOrderAsync(Guid couponId);
    
    public Task AddRatingForOrderAsync(RatingCreateDto ratingCreateDto);
    
    public Task UpdateQuantityAsync(Guid orderId, Guid productId, int quantity);
    
    public Task SetFinalCurrency(Guid orderId, Guid currencyId);
}
