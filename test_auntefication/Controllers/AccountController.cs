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
        private IUserCompanyRepository userCompanyRepository;
        public AccountController(UserManager<AppUser> userMeng, RoleManager<IdentityRole> roleM, SignInManager<AppUser> signIn,
            IUserCompanyRepository ucrep)
        {
            userManager = userMeng;
            roleManager = roleM;
            signInManager = signIn;
            userCompanyRepository = ucrep;
        }
        [AllowAnonymous]
        public ViewResult Index()
        {
            return View();
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
                AppUser resultUser = await userManager.FindByEmailAsync(details.Email);
                if (resultUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await signInManager.PasswordSignInAsync(resultUser, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(LoginModel.Email), "Invalid user or password");
                    }
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
        public async Task<IActionResult> Registration(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.Name, Email = model.Email };
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if(await roleManager.RoleExistsAsync("Admin") == false)
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                if (result.Succeeded)
                {
                    IdentityResult resultRole = await userManager.AddToRoleAsync(user, "Admin");
                    if (resultRole.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("RegistCompany", "Admin");
                    }
                    else
                    {
                        foreach(IdentityError errors in resultRole.Errors)
                        {
                            ModelState.AddModelError("", errors.Description);
                        }
                    }
                    
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ViewResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(UserViewModelTwo model)
        {
            AppUser AdminUser = await userManager.FindByNameAsync(User.Identity.Name);
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.Name, Email = model.Email };
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (await roleManager.RoleExistsAsync("User") == false)
                {
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }
                if (result.Succeeded)
                {
                    IdentityResult resultRole = await userManager.AddToRoleAsync(user, "User");
                    if (resultRole.Succeeded)
                    {
                        userCompanyRepository.AddUserToCompany(AdminUser.Id);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (IdentityError errors in resultRole.Errors)
                        {
                            ModelState.AddModelError("", errors.Description);
                        }
                    }

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
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public ViewResult Delete()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            AppUser user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View();
        }

    }
}
