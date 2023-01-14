using FoodDelivery.BL.DTOs.Category;
using FoodDelivery.BL.DTOs.Currency;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.Facades.ProductFacade;
using FoodDelivery.BL.Facades.RestaurantFacade;
using FoodDelivery.BL.Services.CategoryService;
using FoodDelivery.BL.Services.PriceService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodDelivery.FE.Pages.Forms;

public class EditProduct : PageModel
{
    [BindProperty]
    public ProductUpdateDto Product { get; set; }

    [BindProperty]
    public List<PriceUpdateDto> Prices { get; set; }

    public IEnumerable<CategoryGetDto> Categories { get; set; }

    [BindProperty]
    public string SelectedCategoryId { get; set; }

    public SelectList TagOptionsCategories { get; set; }

    private readonly IProductFacade _productFacade;
    private readonly IRestaurantFacade _restaurantFacade;

    private readonly ICategoryService _categoryService;
    private readonly IPriceService _priceService;

    public EditProduct(IProductFacade productFacade, IRestaurantFacade restaurantFacade,
        ICategoryService categoryService, IPriceService priceService)
    {
        _productFacade = productFacade;
        _restaurantFacade = restaurantFacade;
        _categoryService = categoryService;
        _priceService = priceService;
    }

    public async Task OnGet(Guid productId, Guid restaurantId)
    {
        if (productId != Guid.Empty)
        {
            Product = await _productFacade.GetByIdAsyncAsUpdateDto(productId);
            Prices = Product.Prices;
            SelectedCategoryId = Product.CategoryId.ToString();
        }
        else
        {
            Product = new ProductUpdateDto();
            Product.RestaurantId = restaurantId;
            Prices = new List<PriceUpdateDto>();
            var currencies = await _priceService.GetAllCurrencies();
            foreach (var currency in currencies)
            {
                var price = new PriceUpdateDto();
                price.CurrencyId = currency.Id;
                price.Currency = currency;
                Prices.Add(price);
            }
        }

        Categories = await _categoryService.GetAllAsync();
        TagOptionsCategories =
            new SelectList(Categories.ToList(), nameof(CategoryGetDto.Id), nameof(CategoryGetDto.Name));
    }

    public async Task<IActionResult> OnPost()
    {
        Product.CategoryId = new Guid(SelectedCategoryId);
        if (Product.Id == Guid.Empty)
        {
            Product.Id = Guid.NewGuid();
            foreach (var price in Prices)
            {
                price.Id = Guid.NewGuid();
                price.ProductId = Product.Id;
            }

            await _productFacade.CreateWithNewPrices(Product, Prices);
        }
        else
        {
            await _productFacade.UpdateAsync(Product, Prices);
        }

        return RedirectToPage("/Lists/ProductList", new { restaurantId = Product.RestaurantId });
    }
}
