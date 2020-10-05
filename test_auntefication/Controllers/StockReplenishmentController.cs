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
    public class StockReplenishmentController : Controller
    {
        private ICompanyStockRepository companyStockRepository;
        private UserManager<AppUser> userManager;
        private IUserCompanyRepository userCompanyRepository;
        public StockReplenishmentController(ICompanyStockRepository companyStock, UserManager<AppUser> userMng,
            IUserCompanyRepository userCompany)
        {
            companyStockRepository = companyStock;
            userManager = userMng;
            userCompanyRepository = userCompany;
        }
        public IActionResult AddTabacoToStock()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> AddTabacoToStock(IEnumerable<ViewAddTabacoToStock> details)
        //{
        //    AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
        //    Company companyUser = userCompanyRepository.CompanyToUser(user.Id); 
        //    if(details != null)
        //    {
        //        foreach(ViewAddTabacoToStock det in details)
        //        {
        //            CompanyStock newAddTab = new CompanyStock
        //            {
        //                TabacoName = det.TabacoName,
        //                TabacoBundleWeigh = det.TabacoBundleWeigh,
        //                TabacoCount = det.TabacoCount,
        //                Company = companyUser
        //            };
        //            companyStockRepository.AddCompStock(newAddTab);
        //        }
        //    }
        //    return RedirectToAction("ShowWorkStock", "Display");
        //}
        [HttpPost]
        public async Task<IActionResult> AddTabacoToStock(ViewAddTabacoToStock details)
        {
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            Company companyUser = userCompanyRepository.CompanyToUser(user.Id);
            if (details != null)
            {
                
                    CompanyStock newAddTab = new CompanyStock
                    {
                        TabacoName = details.TabacoName,
                        TabacoBundleWeigh = details.TabacoBundleWeigh,
                        TabacoCount = details.TabacoCount,
                        Company = companyUser
                    };
                    companyStockRepository.AddCompStock(newAddTab);
                
            }
            return RedirectToAction("ShowWorkStock", "Display");
        }
    }
}
