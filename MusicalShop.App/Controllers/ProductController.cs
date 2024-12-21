namespace MusicalShop.App.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using MusicalShop.Services;
    using MusicalShop.Services.Mapping;
    using MusicalShop.Web.InputModels.Product;
    using MusicalShop.Web.ViewModels;
    using MusicalShop.Web.ViewModels.Shop;
    using System;
    using System.Threading.Tasks;
    using System.Linq;

    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("Product/Details/{id?}")]
        public async Task<IActionResult> Details(string id)
        {
            var productDetailsViewModel = (await productService.GetProductByIdAsync(id)).To<ProductDetailsViewModel>();

            if (productDetailsViewModel.Quantity <= 0)
            {
                productDetailsViewModel.IsInStock = false;
            }

            return this.View(productDetailsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Shop()
        {
            var products = productService.GetAllProducts().To<ProductShopViewModel>();
            var productTypes = productService.GetAllProductTypes().To<ProductTypeViewModel>();
            var productBrands = productService.GetAllProductBrands().To<ProductBrandViewModel>();

            ViewData["types"] = productTypes;
            ViewData["brands"] = productBrands;

            return this.View(products);
        }

        [HttpGet("Product/Brand/{brand?}")]
        public async Task<IActionResult> Brand(string brand)
        {
            var products = productService.GetAllProductsByBrand(brand).To<ProductShopViewModel>();
            var productBrands = productService.GetAllProductBrands().To<ProductBrandViewModel>();
            var productTypes = productService.GetAllProductTypes().To<ProductTypeViewModel>();

            ViewData["types"] = productTypes;
            ViewData["brands"] = productBrands;
         
            return this.View("Shop", products);
        }

        [HttpGet("Product/Type/{type?}")]
        public async Task<IActionResult> Type(string type)
        {
            var products = productService.GetAllProductsByType(type).To<ProductShopViewModel>();
            var productBrands = productService.GetAllProductBrands().To<ProductBrandViewModel>();
            var productTypes = productService.GetAllProductTypes().To<ProductTypeViewModel>();

            ViewData["types"] = productTypes;
            ViewData["brands"] = productBrands;

            return this.View("Shop", products);
        }

      /*  [HttpGet("Product/Price/{minPrice?}/{maxPrice?}")]
        public async Task<IActionResult> Price(decimal minPrice, decimal maxPrice)
        {
            var products = productService.GetAllProductsByPrice(minPrice, maxPrice).To<ProductShopViewModel>();
            var productBrands = productService.GetAllProductBrands().To<ProductBrandViewModel>();
            var productTypes = productService.GetAllProductTypes().To<ProductTypeViewModel>();

            ViewData["types"] = productTypes;
            ViewData["brands"] = productBrands;

            return this.View("Shop", products);
        }*/
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromForm] ProductCartInputModel model)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            var key = "product" + Guid.NewGuid().ToString();
            var value = $"Id:{model.Id} Qty:{model.Quantity}";

            var responseCookie = Request.Cookies.FirstOrDefault(x => x.Key.Contains("product") && x.Value.Contains(model.Id));

            if (responseCookie.Key != null)
            {
                var qty = int.Parse(responseCookie.Value.Split(new[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries)[3]) + model.Quantity;
                key = responseCookie.Key;
                value = $"Id:{model.Id} Qty:{qty}";
            }

            var cookieOpts = new CookieOptions();
            cookieOpts.Expires = DateTime.UtcNow.AddDays(7);
            
            Response.Cookies.Append(key, value, cookieOpts);

            return this.Redirect("/");
        }

        [HttpGet("Product/Wishlist/{id?}")]
        public async Task<IActionResult> AddToWishlist(string id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            var key = "wish" + Guid.NewGuid().ToString();
            var value = id;
            var responseCookie = Request.Cookies.FirstOrDefault(x => x.Value == id);
            if (responseCookie.Key == null)
            {
                var cookieOpts = new CookieOptions();
                cookieOpts.Expires = DateTime.UtcNow.AddDays(7);
                var cookie = Request.Cookies[key];

                Response.Cookies.Append(key, value, cookieOpts);
            }

            return this.Redirect("/");
        }
    }
}