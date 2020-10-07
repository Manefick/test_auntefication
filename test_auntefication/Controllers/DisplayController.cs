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
        private ICompanyStockRepository companyStockRepository;
        public DisplayController(UserManager<AppUser> usrMng, IWorkStockRepository wsr, IUserCompanyRepository ucr, 
            ICompanyStockRepository cmpnrep)
        {
            userManager = usrMng;
            workStockRepository = wsr;
            userCompanyRepository = ucr;
            companyStockRepository = cmpnrep;
        }
        public async Task<IActionResult> ShowWorkStock()
        {
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            var result = workStockRepository.DisplayWorkStock(userCompanyRepository.CompanyToUser(user.Id));

            return View(result);
        }
        public async Task<IActionResult> ShowStock()
        {
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            var result = companyStockRepository.DisplayCompanyStock(userCompanyRepository.CompanyToUser(user.Id));
            return View(result);
        }
    }
}
