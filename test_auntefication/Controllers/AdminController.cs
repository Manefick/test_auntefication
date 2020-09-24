using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using test_auntefication.Models;
using test_auntefication.Data;
using Microsoft.AspNetCore.Authorization;

namespace test_auntefication.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        public AdminController(UserManager<AppUser> usrMng, RoleManager<IdentityRole> roleMng)
        {
            userManager = usrMng;
            roleManager = roleMng;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View(userManager.Users);
        }
        public ViewResult ListRole() => View(roleManager.Roles.ToList());
        //public ViewResult Edit() => View();

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> Edit( string email)
        //{
        //    string name = User.Identity.Name;
        //    AppUser user = await userManager.FindByNameAsync(name);
        //    if (user != null)
        //    {
        //        user.Email = email;
        //        IdentityResult result = await userManager.UpdateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return View("Error", "Erorr");
        //}
    }
}
