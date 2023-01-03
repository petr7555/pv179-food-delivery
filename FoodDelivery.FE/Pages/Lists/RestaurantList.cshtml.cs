using FoodDelivery.BL.DTOs.Category;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Facades.ProductFacade;
using FoodDelivery.BL.Facades.RestaurantFacade;
using FoodDelivery.BL.Services.CategoryService;
using FoodDelivery.BL.Services.RatingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodDelivery.FE.Pages.Lists;

[Authorize(Roles = "Customer, ContentManager")]
public class RestaurantList : PageModel
{
    public string NameSort { get; set; }
    public string CurrentFilter { get; set; }
    public IEnumerable<RestaurantGetDto> Restaurants { get; set; }
    public IEnumerable<CategoryGetDto> Categories { get; set; }

    public IEnumerable<SelectListItem> SortOptions { get; set; }

    [BindProperty]
    public string SelectedTagCategory { get; set; }
    [BindProperty]
    public string SelectedTagSortOption { get; set; }
    public SelectList TagOptionsCategory { get; set; }
    public SelectList TagSortOptions { get; set; }

    private readonly IProductFacade _productFacade;
    private readonly IRatingService _ratingService;

    private readonly ICategoryService _categoryService;

    private readonly IRestaurantFacade _restaurantFacade;

    public RestaurantList(IProductFacade productFacade, IRatingService ratingService, ICategoryService categoryService, IRestaurantFacade restaurantFacade)
    {
        _categoryService = categoryService;
        _restaurantFacade = restaurantFacade;
        _productFacade = productFacade;
        _ratingService = ratingService;
    }

    public async Task OnGetAsync(string sortOrder, string searchString, string SelectedTagCategory, string SelectedTagSortOption)
    {
        NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

        CurrentFilter = searchString;
        Categories = await _categoryService.GetAllAsync();
        SortOptions = new List<SelectListItem>() {
            new SelectListItem{ Selected=true, Value="-1", Text=string.Empty },
            new SelectListItem{ Selected=false, Value = "0", Text="Alphabetically" },
            new SelectListItem{ Selected=false, Value = "1", Text="Average Menu Price" },
            new SelectListItem{ Selected=false, Value = "2", Text="Delivery Price" },
            new SelectListItem{ Selected=false, Value = "3", Text="Rating" }
        };

        // sort categories by its tree structure, then alphabetically for each layer
        var currentLayer = Categories.Where(category => !category.ParentCategoryId.HasValue);
        var sortedCategories = new List<CategoryGetDto>();
        while (currentLayer.Any())
        {
            currentLayer = currentLayer.OrderBy(category => category.Name);
            sortedCategories.AddRange(currentLayer);
            var temp = new List<CategoryGetDto>();

            // find all subcategories
            foreach (var category in currentLayer)
            {
                temp.AddRange(Categories.Where(ctg => ctg?.ParentCategoryId == category.Id));
            }

            currentLayer = temp;
        }

        TagOptionsCategory = new SelectList(sortedCategories, nameof(CategoryGetDto.Id), nameof(CategoryGetDto.Name));
        TagSortOptions = new SelectList(SortOptions, "Value", "Text");

        if (SelectedTagCategory != null)
        {
            Restaurants = await _categoryService.GetRestaurantsForCategoryAsync(new Guid(SelectedTagCategory));
        }
        else
        {
            Restaurants = await _restaurantFacade.GetAllAsync();
        }

        if (SelectedTagSortOption != null)
        {
            if (SelectedTagSortOption.Equals("0"))
            {
                Restaurants = Restaurants.OrderBy(restaurant => restaurant.Name);
            }
            else if (SelectedTagSortOption.Equals("1"))
            {
                var products = await _productFacade.GetAllAsync(User.Identity.Name);
                var restaurantsWithAverageCost = new List<KeyValuePair<RestaurantGetDto, float>>();

                for (int i = 0; i < Restaurants.Count(); i++)
                {
                    var restaurantProducts = products.Where((product) => product.Restaurant.Id == Restaurants.ElementAt(i).Id);
                    var averageCost = restaurantProducts.Average(product => product.PricePerEach.Amount);
                    restaurantsWithAverageCost.Add(new KeyValuePair<RestaurantGetDto, float>(Restaurants.ElementAt(i), averageCost));
                }
                restaurantsWithAverageCost = restaurantsWithAverageCost.OrderBy((pair) => pair.Value).ToList();
                Restaurants = restaurantsWithAverageCost.Select(pair => pair.Key);
            }
            else if (SelectedTagSortOption.Equals("2"))
            {
                var currencySortingId = Restaurants.First().DeliveryPrices[0].Currency.Id;
                Restaurants = Restaurants.OrderBy(restaurant => restaurant.DeliveryPrices.Where(price => price.CurrencyId.Equals(currencySortingId)).First().Amount);
            }
            else if (SelectedTagSortOption.Equals("3"))
            {
                var ratings = await _ratingService.GetAllAsync();
                var restaurantsWithRating = new List<KeyValuePair<RestaurantGetDto, double>>();

                for (int i = 0; i < Restaurants.Count(); i++)
                {
                    var restaurantRatings = ratings.Where(rating => rating.RestaurantId.Equals(Restaurants.ElementAt(i).Id));
                    var averageRating = restaurantRatings.Count() == 0 ? 0 : restaurantRatings.Average(rating => rating.Stars);
                    restaurantsWithRating.Add(new KeyValuePair<RestaurantGetDto, double>(Restaurants.ElementAt(i), averageRating));
                }

                restaurantsWithRating = restaurantsWithRating.OrderBy((pair) => pair.Value).ToList();
                Restaurants = restaurantsWithRating.Select(pair => pair.Key);
            }
        }

        if (!String.IsNullOrEmpty(searchString))
        {
            Restaurants = Restaurants.Where(restaurant => restaurant.Name.ToLower().Contains(searchString.ToLower()));
        }

    }
}
