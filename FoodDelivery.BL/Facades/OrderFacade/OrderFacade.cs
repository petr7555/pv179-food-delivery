using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Coupon;
using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.DTOs.OrderProduct;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.DTOs.Rating;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Services.CouponService;
using FoodDelivery.BL.Services.OrderProductService;
using FoodDelivery.BL.Services.OrderService;
using FoodDelivery.BL.Services.ProductService;
using FoodDelivery.BL.Services.RatingService;
using FoodDelivery.BL.Services.UserService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using Stripe.Checkout;
using VetCV.HtmlRendererCore.PdfSharpCore;

namespace FoodDelivery.BL.Facades.OrderFacade;

public class OrderFacade : IOrderFacade
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderService _orderService;
    private readonly IOrderProductService _orderProductService;
    private readonly IUserService _userService;
    private readonly ICouponService _couponService;
    private readonly IRatingService _ratingService;
    private readonly IProductService _productService;

    public OrderFacade(IUnitOfWork unitOfWork, IOrderService orderService, IOrderProductService orderProductService,
        IUserService userService, ICouponService couponService, IRatingService ratingService,
        IProductService productService)
    {
        _unitOfWork = unitOfWork;
        _orderService = orderService;
        _orderProductService = orderProductService;
        _userService = userService;
        _couponService = couponService;
        _ratingService = ratingService;
        _productService = productService;
    }

    private async Task<OrderWithProductsGetDto> OrderToOrderWithProducts(OrderGetDto order)
    {
        var currency = order.CustomerDetails.Customer.UserSettings.SelectedCurrency;
        var products = (await _orderProductService.GetProductsForOrderAsync(order.Id)).ToList();
        var productsLocalized = products.Select(p =>
        {
            var pricePerEach = p.Prices.Single(price => price.Currency.Id == currency.Id);
            return new ProductLocalizedGetDto
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Category = p.Category,
                Restaurant = p.Restaurant,
                Quantity = p.Quantity,
                PricePerEach = pricePerEach,
                TotalPrice = new PriceGetDto
                    { Amount = pricePerEach.Amount * p.Quantity, Currency = pricePerEach.Currency },
            };
        }).ToList();

        var couponsLocalized = order.Coupons.Select(c => new CouponLocalizedGetDto
        {
            Id = c.Id,
            Code = c.Code,
            ValidUntil = c.ValidUntil,
            Status = c.Status,
            Discount = c.Prices.Single(price => price.Currency.Id == currency.Id),
        }).ToList();

        RestaurantLocalizedGetDto? restaurantLocalized = null;
        var product = products.FirstOrDefault();
        if (product != null)
        {
            var restaurant = product.Restaurant;
            restaurantLocalized = new RestaurantLocalizedGetDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                DeliveryPrice = restaurant.DeliveryPrices.Single(price => price.Currency.Id == currency.Id),
                AverageRating = restaurant.Ratings.Any() ? restaurant.Ratings.Average(r => r.Stars) : null,
            };
        }

        var totalAmount = Math.Max(0,
                productsLocalized.Sum(p => p.TotalPrice.Amount) - couponsLocalized.Sum(c => c.Discount.Amount)) +
            restaurantLocalized?.DeliveryPrice.Amount ?? 0;

        return new OrderWithProductsGetDto
        {
            Id = order.Id,
            CreatedAt = order.CreatedAt,
            CustomerDetails = order.CustomerDetails,
            PaymentMethod = order.PaymentMethod,
            Status = order.Status,
            OrderProducts = order.OrderProducts,
            Rating = order.Rating,
            Products = productsLocalized,
            Coupons = couponsLocalized,
            Restaurant = restaurantLocalized,
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
        var activeOrder = await GetActiveOrderAsync(username);

        Guid orderId;
        if (activeOrder != null)
        {
            var existingRestaurantId = activeOrder.Restaurant?.Id;
            var productRestaurantId = (await _productService.GetByIdAsync(productId)).Restaurant.Id;
            if (existingRestaurantId != null && existingRestaurantId != productRestaurantId)
            {
                throw new InvalidOperationException(
                    "Cannot add product from different restaurant to the same order. Please finish the current order first or remove the products from it.");
            }

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

        var existingOrderProduct = await GetOrderProductAsync(orderId, productId);

        if (existingOrderProduct != null)
        {
            _orderProductService.Update(new OrderProductUpdateDto
            {
                Id = existingOrderProduct.Id,
                OrderId = orderId,
                ProductId = productId,
                Quantity = existingOrderProduct.Quantity + 1,
            }, new[] { nameof(OrderProductUpdateDto.Quantity) });
        }
        else
        {
            _orderProductService.Create(new OrderProductCreateDto
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = 1,
            });
        }

        await _unitOfWork.CommitAsync();
    }

    private async Task<OrderProductGetDto?> GetOrderProductAsync(Guid orderId, Guid productId)
    {
        var existingOrderProduct = (await _orderProductService.QueryAsync(new QueryDto<OrderProductGetDto>()
            .Where(op => op.OrderId == orderId && op.ProductId == productId))).SingleOrDefault();
        return existingOrderProduct;
    }

    public async Task<OrderWithProductsGetDto?> GetActiveOrderAsync(string username)
    {
        var orders = await GetOrdersForUserAsync(username);
        var activeOrder = orders.SingleOrDefault(o => o.Status == OrderStatus.Active);
        return activeOrder;
    }

    private async Task ChangeOrderStatusAsync(Guid orderId, OrderStatus status)
    {
        var order = await _orderService.GetByIdAsync(orderId);
        var updatedOrder = new OrderUpdateDto
        {
            Id = order.Id,
            Status = status,
        };
        _orderService.Update(updatedOrder, new[] { nameof(OrderUpdateDto.Status) });
        await _unitOfWork.CommitAsync();
    }

    public async Task FulfillOrderAsync(Guid orderId)
    {
        await ChangeOrderStatusAsync(orderId, OrderStatus.Paid);
    }

    public async Task SubmitOrderAsync(Guid orderId)
    {
        await ChangeOrderStatusAsync(orderId, OrderStatus.Submitted);
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
        _couponService.Update(couponUpdateDto,
            new[] { nameof(CouponUpdateDto.Status), nameof(CouponUpdateDto.OrderId) });
        await _unitOfWork.CommitAsync();
    }

    public async Task<string> PayByCardAsync(OrderWithProductsGetDto order, string domain)
    {
        var products = order.Products;
        var sessionLineItemOptions = products.Select(p => new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmount = (long)(p.PricePerEach.Amount * 100), 
                Currency = p.PricePerEach.Currency.Name,
                ProductData = new SessionLineItemPriceDataProductDataOptions { Name = p.Name },
            },
            Quantity = p.Quantity,
        }).ToList();

        var options = new SessionCreateOptions
        {
            LineItems = sessionLineItemOptions,
            Mode = "payment",
            SuccessUrl = domain + "/Payment/Success",
            CancelUrl = domain + "/Payment/Cancel",
            ClientReferenceId = order.Id.ToString(),
            CustomerEmail = order.CustomerDetails.Customer.Email,
            Currency = order.TotalPrice.Currency.Name,
            Discounts = order.Coupons.Select(c => new SessionDiscountOptions
                {
                    Coupon = c.Code,
                }
            ).ToList(),
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return session.Url;
    }

    public async Task SetPaymentMethodAsync(Guid orderId, PaymentMethod paymentMethod)
    {
        var order = await _orderService.GetByIdAsync(orderId);
        var updatedOrder = new OrderUpdateDto
        {
            Id = order.Id,
            PaymentMethod = paymentMethod,
        };
        _orderService.Update(updatedOrder, new[] { nameof(OrderUpdateDto.PaymentMethod) });
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteProductFromOrderAsync(OrderWithProductsGetDto order, Guid productId)
    {
        var orderProductToDelete = order.OrderProducts.First(op => op.ProductId == productId);
        _orderProductService.Delete(orderProductToDelete.Id);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteCouponFromOrderAsync(Guid couponId)
    {
        var couponUpdateDto = new CouponUpdateDto
        {
            Id = couponId,
            Status = CouponStatus.Valid,
            OrderId = null,
        };
        _couponService.Update(couponUpdateDto,
            new[] { nameof(CouponUpdateDto.Status), nameof(CouponUpdateDto.OrderId) });
        await _unitOfWork.CommitAsync();
    }

    public async Task AddRatingForOrderAsync(RatingCreateDto ratingCreateDto)
    {
        _ratingService.Create(ratingCreateDto);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateQuantityAsync(Guid orderId, Guid productId, int quantity)
    {
        var existingOrderProduct = await GetOrderProductAsync(orderId, productId);
        if (existingOrderProduct is null)
        {
            throw new InvalidOperationException(
                $"OrderProduct for order {orderId} and product {productId} does not exist.");
        }

        _orderProductService.Update(new OrderProductUpdateDto
        {
            Id = existingOrderProduct.Id,
            OrderId = orderId,
            ProductId = productId,
            Quantity = quantity,
        }, new[] { nameof(OrderProductUpdateDto.Quantity) });

        await _unitOfWork.CommitAsync();
    }
}
