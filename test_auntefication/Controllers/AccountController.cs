using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using test_auntefication.Models;
using Microsoft.AspNetCore.Identity;
namespace test_auntefication.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private SignInManager<AppUser> signInManager;
        public AccountController(UserManager<AppUser> userMeng, RoleManager<IdentityRole> roleM, SignInManager<AppUser> signIn)
        {
            userManager = userMeng;
            roleManager = roleM;
            signInManager = signIn;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = 
                    await signInManager.PasswordSignInAsync(details.Email, details.Password, false,false);
                if (result.Succeeded)
                {
                    return Redirect(returnUrl ?? "/");
                }
                else
                {
                    ModelState.AddModelError(nameof(LoginModel.Email), "Invalid user or password");
                }
            }
            return View(details);
        }
        [HttpGet]
        [AllowAnonymous]
        public ViewResult Registration(string retutnUrl)
        {
            ViewBag.returnUrl = retutnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(UserViewModel model, string returnUrl = null)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.Name, Email = model.Email };
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect(returnUrl ?? "/");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);

        }

    }
}
