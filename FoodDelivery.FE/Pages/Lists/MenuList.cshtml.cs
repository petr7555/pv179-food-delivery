using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.Facades.ProductFacade;
using Microsoft.AspNetCore.Authorization;

namespace FoodDelivery.FE.Pages.Lists
{
    [Authorize(Roles = "Customer, ContentManager")]
    public class MenuList : PageModel
    {
        public IEnumerable<ProductGetDto> Products { get; set; }

        private readonly IProductFacade _productFacade;

        public MenuList(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }
        public async void OnGet(string id)
        {
            Products = await _productFacade.GetAllAsync();

            Products.Where(product => product.Restaurant.Id == new Guid(id));
        }
    }
}
