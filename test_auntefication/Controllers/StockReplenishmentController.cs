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
        private IWorkStockRepository workStockRepository;
        public StockReplenishmentController(ICompanyStockRepository companyStock, UserManager<AppUser> userMng,
            IUserCompanyRepository userCompany, IWorkStockRepository workStock)
        {
            companyStockRepository = companyStock;
            userManager = userMng;
            userCompanyRepository = userCompany;
            workStockRepository = workStock;
        }
        public IActionResult AddTabacoToStock()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTabacoToStock(ViewAddTabacoToStockList details)
        {
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            Company companyUser = userCompanyRepository.CompanyToUser(user.Id);
            List<CompanyStock> data = new List<CompanyStock>();
            if (details != null)
            {
                foreach (ViewAddTabacoToStock det in details.tabacoToStocks)
                {
                    CompanyStock repitCompStock = companyStockRepository.DisplayCompanyStock(companyUser).Where(
                        p => String.Equals(p.TabacoName, det.TabacoName, StringComparison.OrdinalIgnoreCase) &&
                        p.TabacoBundleWeigh == det.TabacoBundleWeigh).FirstOrDefault();
                    if (repitCompStock != null)
                    {
                        repitCompStock.TabacoCount += det.TabacoCount;
                        companyStockRepository.EditCompanyStock(repitCompStock);
                    }
                    else
                    {
                        if (det.TabacoName != null)
                        {
                            data.Add(new CompanyStock
                            {
                                TabacoName = det.TabacoName,
                                TabacoBundleWeigh = det.TabacoBundleWeigh,
                                TabacoCount = det.TabacoCount,
                                Company = companyUser
                            });
                        }
                    }
                }
                companyStockRepository.AddCompStocks(data);
            }
            return RedirectToAction("ShowStock", "Display");
        }
        //[HttpPost]
        //public async Task<IActionResult> AddTabacoToStock(ViewAddTabacoToStock details)
        //{
        //    AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
        //    Company companyUser = userCompanyRepository.CompanyToUser(user.Id);
        //    foreach(CompanyStock cs in companyStockRepository.DisplayCompanyStock(companyUser))
        //    {
        //        if(cs.TabacoName == details.TabacoName&& cs.TabacoBundleWeigh == details.TabacoBundleWeigh)
        //        {
        //            cs.TabacoCount = cs.TabacoCount + details.TabacoCount;
        //            companyStockRepository.EditCompanyStock(cs);
        //            return RedirectToAction("ShowStock", "Display");
        //        }
        //    }
        //        CompanyStock newAddTab = new CompanyStock
        //        {
        //            TabacoName = details.TabacoName,
        //            TabacoBundleWeigh = details.TabacoBundleWeigh,
        //            TabacoCount = details.TabacoCount,
        //            Company = companyUser
        //        };
        //        companyStockRepository.AddCompStock(newAddTab);


        //    return RedirectToAction("ShowStock", "Display");
        //}
        public async Task<IActionResult> AddTabacoToWorkStock()
        {
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            var result = companyStockRepository.DisplayCompanyStock(userCompanyRepository.CompanyToUser(user.Id));
            return View(new AddTabacoToWorkStock {CompanyStock = result});
        }
        [HttpPost]
        public async Task<IActionResult> AddTabacoToWorkStock(AddTabacoToWorkStock details)
        {
            //AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            //Company companyUser = userCompanyRepository.CompanyToUser(user.Id);
            var res = details.CompanyStock;
            //CompanyStock companyStock = companyStockRepository.DisplayCompanyStock(companyUser)
            //    .Where(p => p.Id == details.CompanyStock.First().Id).FirstOrDefault();
            //companyStock.TabacoCount -= details.CountTabacoPack;
            //companyStockRepository.EditCompanyStock(companyStock);
            //workStockRepository.AddWorkStock(new WorkStock
            //{
            //    Company = companyStock.Company,
            //    NameTabaco = companyStock.TabacoName,
            //    TabacoWeigh = details.TabacoWeigth
            //});
            return View("test", res);
        }
    }
}
