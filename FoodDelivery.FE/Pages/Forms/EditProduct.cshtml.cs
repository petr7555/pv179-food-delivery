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

namespace FoodDelivery.FE.Pages.Forms
{
    public class EditProduct : PageModel
    {
        [BindProperty]
        public ProductUpdateDto Product { get; set; }

        public IEnumerable<CurrencyGetDto> Currencies { get; set; }
        public IEnumerable<CategoryGetDto> Categories { get; set; }

        [BindProperty]
        public string SelectedTagCategory { get; set; }
        [BindProperty]
        public string SelectedTagCurrency { get; set; }

        public SelectList TagOptionsCategories { get; set; }
        public SelectList TagOptionsCurrencies { get; set; }
        
        private readonly IProductFacade _productFacade;
        private readonly IRestaurantFacade _restaurantFacade;

        private readonly ICategoryService _categoryService;
        private readonly IPriceService _priceService;

        public EditProduct(IProductFacade productFacade, IRestaurantFacade restaurantFacade, ICategoryService categoryService, IPriceService priceService)
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
            }
            else
            {
                Product = new ProductUpdateDto();
                Product.Price = new PriceUpdateDto();
                Product.RestaurantId = restaurantId;
            }

            Console.WriteLine("Price ID (ONGET): " + Product.Price.Id);

            Currencies = await _priceService.GetAllCurrencies();
            Categories = await _categoryService.GetAllAsync();

            TagOptionsCurrencies = new SelectList(Currencies.ToList(), nameof(CurrencyGetDto.Id), nameof(CurrencyGetDto.Name));
            TagOptionsCategories = new SelectList(Categories.ToList(), nameof(CategoryGetDto.Id), nameof(CategoryGetDto.Name));
        }

        public async Task<IActionResult> OnPost()
        {
            Console.WriteLine("Price ID: " + Product.Price.Id);

            Product.CategoryId = new Guid(SelectedTagCategory);
            Product.Price.CurrencyId = new Guid(SelectedTagCurrency);

            if (Product.Id == Guid.Empty)
            {
                Product.Id = new Guid();
                Product.Price.Id = new Guid();
                Product.PriceId = Product.Price.Id;                
                await _productFacade.Create(Product);
            }
            else
            {
                await _productFacade.Update(Product);
            }
            return RedirectToPage("/Lists/ProductList", new { restaurantId = Product.RestaurantId });
        }

        public async Task<string> getCategoryName()
        {
            if (Product.CategoryId == Guid.Empty)
            {
                return "";
            }
            var category = await _categoryService.GetByIdAsync(Product.CategoryId);
            return category.Name;
        }
    }
}
