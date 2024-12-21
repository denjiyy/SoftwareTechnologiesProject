namespace MusicalShop.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MusicalShop.Services;
    using MusicalShop.Services.Mapping;
    using MusicalShop.Web.ViewModels.Wishlist;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class WishlistController : Controller
    {
        private readonly IProductService productService;

        public WishlistController(IProductService productService)
        {
            this.productService = productService;
        }
        public async Task<IActionResult> Wishlist()
        {
            var wishes = await GetProductsFromCookies();
            return this.View(wishes);
        }
        public async Task<List<WishlistViewModel>> GetProductsFromCookies()
        {
            var cookies = Request.Cookies.Where(x => x.Key.Contains("wish"));
            var wishes = new List<WishlistViewModel>();

            foreach (var cookie in cookies)
            { 
                var id = cookie.Value;

                if (!wishes.Where(x => x.Id == id).Any())
                {
                    var wish = (await productService.GetProductByIdAsync(id)).To<WishlistViewModel>();
                    wishes.Add(wish);
                }
            }

            return wishes;
        }
        [HttpGet("Wishlist/Remove/{id?}")]
        public async Task<IActionResult> RemoveWish(string id)
        {
            var cookie = Request.Cookies.SingleOrDefault(x => x.Value == id);
            var key = cookie.Key;
            Response.Cookies.Delete(key);
            return this.Redirect("/Wishlist/Wishlist");
        }
    }
}