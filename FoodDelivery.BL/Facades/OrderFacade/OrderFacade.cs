using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.DTOs.OrderProduct;
using FoodDelivery.BL.Services.OrderProductService;
using FoodDelivery.BL.Services.OrderService;
using FoodDelivery.BL.Services.UserService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Facades.OrderFacade;

public class OrderFacade : IOrderFacade
{
    private readonly IUnitOfWork _uow;
    private readonly IOrderService _orderService;
    private readonly IOrderProductService _orderProductService;
    private readonly IUserService _userService;

    public OrderFacade(IUnitOfWork uow, IOrderService orderService, IOrderProductService orderProductService,
        IUserService userService)
    {
        _uow = uow;
        _orderService = orderService;
        _orderProductService = orderProductService;
        _userService = userService;
    }

    public async Task<OrderWithProductsGetDto?> GetByIdAsync(Guid id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order == null)
        {
            return null;
        }

        var orderWithProducts = new OrderWithProductsGetDto
        {
            Id = order.Id,
            CreatedAt = order.CreatedAt,
            CustomerDetails = order.CustomerDetails,
            Status = order.Status,
            Products = (await _orderProductService.GetProductsForOrderAsync(order.Id)).ToList(),
        };
        return orderWithProducts;
    }

    public async Task<IEnumerable<OrderWithProductsGetDto>> GetOrdersForUserAsync(string username)
    {
        var user = await _userService.GetByUsernameAsync(username);
        var orders = await _orderService.QueryAsync(
            new QueryDto<OrderGetDto>()
                .Where(o => o.CustomerDetails.Customer.Id == user.Id));
        var ordersWithProducts = new List<OrderWithProductsGetDto>();
        foreach (var order in orders)
        {
            ordersWithProducts.Add(new OrderWithProductsGetDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                CustomerDetails = order.CustomerDetails,
                Status = order.Status,
                Products = (await _orderProductService.GetProductsForOrderAsync(order.Id)).ToList(),
            });
        }

        return ordersWithProducts;
    }

    public async Task AddProductToCartAsync(string username, Guid productId)
    {
        var user = await _userService.GetByUsernameAsync(username);
        var orders = await GetOrdersForUserAsync(username);
        var activeOrder = orders.SingleOrDefault(o => o.Status == OrderStatus.Active);

        Guid orderId;
        if (activeOrder != null)
        {
            orderId = activeOrder.Id;
        }
        else
        {
            var newOrder = new OrderCreateDto
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                // TODO what if not found?
                CustomerDetailsId = user.CustomerDetailsId ??
                                    throw new InvalidOperationException("Customer details not found"),
                Status = OrderStatus.Active,
            };
            _orderService.Create(newOrder);
            orderId = newOrder.Id;
        }

        _orderProductService.Create(new OrderProductCreateDto
        {
            OrderId = orderId,
            ProductId = productId,
        });

        await _uow.CommitAsync();
    }

    public async Task<OrderWithProductsGetDto?> GetActiveOrderAsync(string username)
    {
        var orders = await GetOrdersForUserAsync(username);
        var activeOrder = orders.SingleOrDefault(o => o.Status == OrderStatus.Active);
        return activeOrder;
    }

    public async Task FulfillOrderAsync(Guid orderId)
    {
        var order = await _orderService.GetByIdAsync(orderId);
        var updatedOrder = new OrderUpdateDto
        {
            Id = order.Id,
            Status = OrderStatus.Paid,
        };
        _orderService.Update(updatedOrder);
        await _uow.CommitAsync();
    }
}
