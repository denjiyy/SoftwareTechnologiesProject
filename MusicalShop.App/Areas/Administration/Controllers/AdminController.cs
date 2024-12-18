namespace MusicalShop.App.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Admin")]
    [Area("Administration")]
    public class AdminController : Controller
    {
    }
}