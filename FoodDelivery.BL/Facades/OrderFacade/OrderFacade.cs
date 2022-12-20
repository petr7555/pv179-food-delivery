using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Coupon;
using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.DTOs.OrderProduct;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.Services.CouponService;
using FoodDelivery.BL.Services.OrderProductService;
using FoodDelivery.BL.Services.OrderService;
using FoodDelivery.BL.Services.UserService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using VetCV.HtmlRendererCore.PdfSharpCore;

namespace FoodDelivery.BL.Facades.OrderFacade;

public class OrderFacade : IOrderFacade
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderService _orderService;
    private readonly IOrderProductService _orderProductService;
    private readonly IUserService _userService;
    private readonly ICouponService _couponService;

    public OrderFacade(IUnitOfWork unitOfWork, IOrderService orderService, IOrderProductService orderProductService,
        IUserService userService, ICouponService couponService)
    {
        _unitOfWork = unitOfWork;
        _orderService = orderService;
        _orderProductService = orderProductService;
        _userService = userService;
        _couponService = couponService;
    }

    private async Task<OrderWithProductsGetDto> OrderToOrderWithProducts(OrderGetDto order)
    {
        var currency = order.CustomerDetails.Customer.UserSettings.SelectedCurrency;
        var products = (await _orderProductService.GetProductsForOrderAsync(order.Id)).ToList();
        var productsLocalized = products.Select(p => new ProductLocalizedGetDto()
        {
            Id = p.Id,
            Name = p.Name,
            ImageUrl = p.ImageUrl,
            Category = p.Category,
            Restaurant = p.Restaurant,
            Price = p.Prices.Single(price => price.Currency.Id == currency.Id),
        }).ToList();

        var couponsLocalized = order.Coupons.Select(c => new CouponLocalizedGetDto
        {
            Id = c.Id,
            Code = c.Code,
            ValidUntil = c.ValidUntil,
            Status = c.Status,
            Price = c.Prices.Single(price => price.Currency.Id == currency.Id), 
        }).ToList();
        
        var totalAmount = Math.Max(0, productsLocalized.Sum(p => p.Price.Amount) - couponsLocalized.Sum(c => c.Price.Amount));
        return new OrderWithProductsGetDto
        {
            Id = order.Id,
            CreatedAt = order.CreatedAt,
            CustomerDetails = order.CustomerDetails,
            Status = order.Status,
            Products = productsLocalized,
            Coupons = couponsLocalized,
            TotalPrice = new PriceGetDto
            {
                Amount = totalAmount,
                Currency = currency,
            },
        };
    }

    public async Task<OrderWithProductsGetDto?> GetByIdAsync(Guid id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order == null)
        {
            return null;
        }

        return await OrderToOrderWithProducts(order);
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
            ordersWithProducts.Add(await OrderToOrderWithProducts(order));
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

        await _unitOfWork.CommitAsync();
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
        _orderService.Update(updatedOrder, new[] { nameof(OrderUpdateDto.Status) });
        await _unitOfWork.CommitAsync();
    }

    public async Task<MemoryStream> CreatePdfFromOrder(string url)
    {
        var httpClient = new HttpClient();
        var html = await httpClient.GetStringAsync(url);
        var pdfDocument = PdfGenerator.GeneratePdf(html, PageSize.A4, (int)PdfPageMode.UseOutlines);
        var stream = new MemoryStream();
        pdfDocument.Save(stream, false);
        return stream;
    }

    public async Task ApplyCouponCodeAsync(Guid orderId, string couponCode)
    {
        var coupon = (await _couponService.QueryAsync(new QueryDto<CouponGetDto>()
            .Where(c => c.Code == couponCode))).SingleOrDefault();
        if (coupon is not { Status: CouponStatus.Valid } || coupon.ValidUntil < DateTime.UtcNow)
        {
            throw new InvalidOperationException($"Coupon {couponCode} is not valid");
        }
        var couponUpdateDto = new CouponUpdateDto
        {
            Id = coupon.Id,
            Status = CouponStatus.Used,
            OrderId = orderId,
        };
        _couponService.Update(couponUpdateDto, new[] { nameof(CouponUpdateDto.Status), nameof(CouponUpdateDto.OrderId) });
        await _unitOfWork.CommitAsync();
    }
}
