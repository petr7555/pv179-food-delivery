using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.DTOs.OrderProduct;
using FoodDelivery.BL.Services.OrderProductService;
using FoodDelivery.BL.Services.OrderService;
using FoodDelivery.BL.Services.ProductService;
using FoodDelivery.BL.Services.UserService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Facades.OrderFacade;

public class OrderFacade : IOrderFacade
{
    private readonly IUnitOfWork _uow;
    private readonly IOrderService _orderService;
    private readonly IOrderProductService _orderProductService;
    private readonly IProductService _productService;
    private readonly IUserService _userService;

    public OrderFacade(IUnitOfWork uow, IOrderService orderService, IOrderProductService orderProductService,
        IProductService productService,
        IUserService userService)
    {
        _uow = uow;
        _orderService = orderService;
        _orderProductService = orderProductService;
        _productService = productService;
        _userService = userService;
    }

    public async Task<IEnumerable<OrderGetDto>> GetAllAsync()
    {
        return await _orderService.GetAllAsync();
    }

    public async Task<IEnumerable<OrderGetDto>> QueryAsync(QueryDto<OrderGetDto> queryDto)
    {
        return await _orderService.QueryAsync(queryDto);
    }

    public async Task<IEnumerable<OrderGetDto>> GetOrdersForUserAsync(string username)
    {
        var user = await _userService.GetByUsernameAsync(username);
        return await _orderService.QueryAsync(
                new QueryDto<OrderGetDto>()
                    .Where(o => o.CustomerDetails.Customer.Id == user.Id));
    }

    public async Task AddToCartAsync(string username, Guid productId)
    {
        var user = await _userService.GetByUsernameAsync(username);
        var orders = await GetOrdersForUserAsync(username);
        var activeOrder = orders.SingleOrDefault(o => o.OrderStatus == OrderStatus.Active);

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
                // TODO what if not found?
                CustomerDetailsId = user.CustomerDetailsId ??
                                    throw new InvalidOperationException("Customer details not found"),
                OrderStatus = OrderStatus.Active,
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

    public async Task<OrderGetDto?> GetActiveOrder(string username)
    {
        var orders = await GetOrdersForUserAsync(username);
        var activeOrder = orders.SingleOrDefault(o => o.OrderStatus == OrderStatus.Active);
        if (activeOrder == null)
        {
            return null;
        }
        activeOrder.Products = (await _orderProductService.GetProductsForOrderAsync(activeOrder.Id)).ToList();
        return activeOrder;
    }
}
