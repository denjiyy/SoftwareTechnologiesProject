namespace MusicalShop.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MusicalShop.Services;
    using MusicalShop.Services.Mapping;
    using MusicalShop.Web.ViewModels.Order;
    using System;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Collections.Generic;

    public class CartController : Controller
    {
        private readonly IProductService productService;

        public CartController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> Cart()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Register");
            }

            var products = await GetProductsFromCookies();
            decimal price = 0;
            products.ToList().ForEach(p => price += p.Price * p.Quntity);
            ViewData["price"] = price;

            return this.View(products);
        }

        public async Task<IActionResult> Checkout()
        {
            var products = await GetProductsFromCookies();
            decimal price = 0;
            products.ToList().ForEach(p => price += p.Price * p.Quntity);
            ViewData["price"] = price;
            return this.View();
        }
        public async Task<List<CartViewModel>> GetProductsFromCookies()
        {
            var cookies = Request.Cookies.Where(x => x.Key.Contains("product"));
            var products = new List<CartViewModel>();

            foreach (var cookie in cookies)
            {
                var parametres = cookie.Value.Split(new[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
                var id = parametres[1];
                var qty = int.Parse(parametres[3]);

                var product = (await productService.GetProductByIdAsync(id)).To<CartViewModel>();
                product.Quntity = qty;
                products.Add(product);
            }

            return products;
        }
        [HttpGet("Cart/Remove/{id?}")]
        public async Task<IActionResult> Remove(string id)
        {
            var key = Request.Cookies.SingleOrDefault(x => x.Value.Contains(id)).Key;
            Response.Cookies.Delete(key);
            return this.Redirect("/Cart/Cart");
        }
    }
}