namespace MusicalShop.App.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.Facebook;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MusicalShop.Services;
    using MusicalShop.Web.ViewModels.Home;
    using System.Linq;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private const int NeededProductsToListHomePage = 10;
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllProducts()
                .Select(x => Mapper.Map<ProductHomeViewModel>(x))
                .Take(NeededProductsToListHomePage)
                .ToListAsync();

            return View(products);
        }
     /*   [Route("facebook-login")]
        public async Task<IActionResult> FacebookLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("FacebookResponse")
            };
            return Challenge(properties, FacebookDefaults.AuthenticationScheme);
        }
        [Route("facebook-response")]
        public async Task<IActionResult> FacebookResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
            return Json(claims);
        }*/
    }
}