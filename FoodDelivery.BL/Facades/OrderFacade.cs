using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.DTOs.OrderProduct;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.Services.OrderProductService;
using FoodDelivery.BL.Services.OrderService;
using FoodDelivery.BL.Services.ProductService;
using FoodDelivery.BL.Services.UserService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Facades;

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
        var orders =
            await _orderService.QueryAsync(
                new QueryDto<OrderGetDto>().Where(o => o.CustomerDetails.Customer.Id == user.Id));
        return orders;
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

    public async Task<IEnumerable<ProductGetDto>> GetProductsInBasketAsync(string username)
    {
        var orders = await GetOrdersForUserAsync(username);
        var activeOrder = orders.SingleOrDefault(o => o.OrderStatus == OrderStatus.Active);
        if (activeOrder == null)
        {
            return new List<ProductGetDto>();
        }

        var allProducts = await _productService.GetAllAsync();
        return activeOrder.OrderProducts.Select(op => allProducts.Single(p => p.Id == op.ProductId));
    }
}
