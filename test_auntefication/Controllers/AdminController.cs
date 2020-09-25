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
        public async Task<IActionResult> Index()
        {
            Dictionary<string, Dictionary<string,string>> InfoUser = new Dictionary<string, Dictionary<string,string>>();
            string[] RoleList = new string[] { "Admin" };
            foreach (var role in RoleList)
            {
                foreach (var user in userManager.Users)
                {
                    if(await userManager.IsInRoleAsync(user, role))
                    {
                        InfoUser[user.Id] = new Dictionary<string, string> {
                            { "Name", user.UserName },{"Email" ,user.Email }, {"Id", user.Id}, { "Role", role } };
                    }
                    else
                    {
                        InfoUser[user.Id] = new Dictionary<string, string> {
                            { "Name", user.UserName },{"Email" ,user.Email }, {"Id", user.Id}, { "Role", "Null" } };
                    }
                }
            }
            return View(InfoUser);
        }
        public ViewResult ListRole() => View(roleManager.Roles.ToList());
        
    }
}
