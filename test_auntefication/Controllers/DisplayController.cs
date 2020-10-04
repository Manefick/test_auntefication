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
    [Authorize]
    public class DisplayController : Controller
    {
        private UserManager<AppUser> userManager;
        private IWorkStockRepository workStockRepository;
        private IUserCompanyRepository userCompanyRepository;
        public DisplayController(UserManager<AppUser> usrMng, IWorkStockRepository wsr, IUserCompanyRepository ucr)
        {
            userManager = usrMng;
            workStockRepository = wsr;
            userCompanyRepository = ucr;
        }
        public async Task<IActionResult> ShowWorkStock()
        {
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            var result = workStockRepository.DisplayWorkStock(userCompanyRepository.CompanyToUser(user.Id));

            return View(result);
        }
    }
}
