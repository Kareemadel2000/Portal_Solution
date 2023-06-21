using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Portal.PL.Language;
using System.Security.Claims;

namespace Portal.PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<SharedResource> sharedLocalizer;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(UserManager<IdentityUser> userManager, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _userManager = userManager;
            this.sharedLocalizer = sharedLocalizer;
        }

        public async Task<IActionResult> Index()
        {
            var claimsPrincipal = HttpContext.User;
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            ViewBag.Email = user.Email;
            ViewBag.Username = user.UserName;
            return View();
        }
    }
}
