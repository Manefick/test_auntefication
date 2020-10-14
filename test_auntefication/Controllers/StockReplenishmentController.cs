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
        public async Task<IActionResult> AddTabacoToWorkStock()
        {
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            IEnumerable<CompanyStock> result = companyStockRepository.DisplayCompanyStock(userCompanyRepository.CompanyToUser(user.Id));
            List<ViewCompanyStock> viewCompanyStocks = new List<ViewCompanyStock>();
            foreach(var res in result)
            {
                viewCompanyStocks.Add(new ViewCompanyStock
                {
                    Id = res.Id,
                    CompanyId = res.CompanyId,
                    TabacoName = res.TabacoName,
                    TabacoBundleWeigh = res.TabacoBundleWeigh,
                    TabacoCount = res.TabacoCount
                });
            }
            return View(new AddTabacoToWorkStock {CompanyStock = viewCompanyStocks});
        }
        [HttpPost]
        public async Task<IActionResult> AddTabacoToWorkStock(AddTabacoToWorkStock details)
        {
            //добавить вылезающие ошибки при вводе некоректной инфи
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            Company companyUser = userCompanyRepository.CompanyToUser(user.Id);
            CompanyStock companyStock = companyStockRepository.DisplayCompanyStock(companyUser)
                .Where(p => p.Id == details.SelectedCompanyStock).FirstOrDefault();
            if (companyStock.TabacoCount > details.CountTabacoPack)
            {
                companyStock.TabacoCount -= details.CountTabacoPack;
                companyStockRepository.EditCompanyStock(companyStock);
                workStockRepository.AddWorkStock(new WorkStock
                {
                    Company = companyStock.Company,
                    NameTabaco = companyStock.TabacoName,
                    TabacoWeigh = details.TabacoWeigth,
                    Data = DateTime.Now
                });
            }
            return RedirectToAction("ShowWorkStock", "Display");
        }
        public async Task<IActionResult> WriteOff()
        {
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            IEnumerable<CompanyStock> result = companyStockRepository.DisplayCompanyStock(userCompanyRepository.CompanyToUser(user.Id));
            List<ViewCompanyStock> viewCompanyStocks = new List<ViewCompanyStock>();
            foreach (var res in result)
            {
                viewCompanyStocks.Add(new ViewCompanyStock
                {
                    Id = res.Id,
                    CompanyId = res.CompanyId,
                    TabacoName = res.TabacoName,
                    TabacoBundleWeigh = res.TabacoBundleWeigh,
                    TabacoCount = res.TabacoCount
                });
            }
            return View(new AddTabacoToWorkStock { CompanyStock = viewCompanyStocks });
        }
        [HttpPost]
        public async Task<IActionResult> WriteOff(AddTabacoToWorkStock det)
        {
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            Company company = userCompanyRepository.CompanyToUser(user.Id);
            CompanyStock companyStock = companyStockRepository.DisplayCompanyStock(company)
                .Where(p => p.Id == det.SelectedCompanyStock).FirstOrDefault();
            workStockRepository.AddWorkStock(new WorkStock
            {
                Company = company,
                NameTabaco = companyStock.TabacoName,
                TabacoWeigh = -det.TabacoWeigth,
                Data = DateTime.Now
            });
            return RedirectToAction("ShowWorkStock", "Display");
        }
    }
}
