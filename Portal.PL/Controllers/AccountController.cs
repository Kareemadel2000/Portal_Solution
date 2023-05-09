using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portal.Application.Dtos;
using Portal.Domains.Enums;

namespace Portal.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        #region Registration
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> AddUser(string message = null)
        {
            ViewBag.Message = message;
            var roles = await _roleManager.Roles.Where(x => x.Name != Roles.Client.ToString()).ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
            return View();
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(AddUserDto model)
        {
            var user = new IdentityUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            var role = await _roleManager.FindByIdAsync(model.RoleId);
            if(role == null)
            {
                ModelState.AddModelError("", "Role Not Exist.");
            }
            else
            {
                result = await _userManager.AddToRoleAsync(user, role.Name!);
                if (result.Succeeded)
                {                    
                    return RedirectToAction(nameof(AddUser), new {message = "User Added Successfully" });
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            
            var roles = await _roleManager.Roles.Where(x => x.Name != Roles.Client.ToString()).ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
            return View(model);

        }

        #endregion
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.UserName) ?? await _userManager.FindByNameAsync(model.UserName);

            if (user == null || await _userManager.IsInRoleAsync(user, Roles.Client.ToString()))
            {
                ModelState.AddModelError("", "Invalid Login");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if(result.Succeeded)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Invalid UserName Or Password");
            return View(model);            
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();            
            return RedirectToAction(nameof(Login));
        }

        #region Roles
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult AddRole()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleVM roleVM)
        {
            if(await _roleManager.RoleExistsAsync(roleVM.Name) || !ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid Role");
                return View(roleVM);
            }
            var result = await _roleManager.CreateAsync(new IdentityRole { Name = roleVM.Name });
            if(result.Succeeded)
            {
                ViewBag.Message = "Role Added Successfully";
                return View();
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(roleVM);
        }
        #endregion
    }
}
