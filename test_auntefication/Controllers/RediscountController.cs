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
    [Authorize(Roles ="Admin")]
    public class RediscountController : Controller
    {
        private ICompanyStockRepository companyStockRepository;
        private UserManager<AppUser> userManager;
        private IUserCompanyRepository userCompanyRepository;
        private IWorkStockRepository workStockRepository;
        public RediscountController(ICompanyStockRepository companyStock, UserManager<AppUser> userMng,
            IUserCompanyRepository userCompany, IWorkStockRepository workStock)
        {
            companyStockRepository = companyStock;
            userManager = userMng;
            userCompanyRepository = userCompany;
            workStockRepository = workStock;
        }
        public IActionResult RedisCount()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RedisCount(ViewRedisCount details)
        {
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            Company company = userCompanyRepository.CompanyToUser(user.Id);
            WorkStock lastRediscount = workStockRepository.DisplayWorkStock(userCompanyRepository.CompanyToUser(user.Id)).Where(
                p => p.NameTabaco == "REDISCOUNT").LastOrDefault();
            if (lastRediscount != null)
            {
                int sumTabacoWeigh = workStockRepository.DisplayWorkStock(userCompanyRepository.CompanyToUser(user.Id)).Where(
                    p => lastRediscount.Data <= p.Data).Sum(p => p.TabacoWeigh);
                int useTabaco = details.CountHookah * details.Gramovka;
                int teorFinalTabacoWeigh = sumTabacoWeigh - useTabaco;
                workStockRepository.AddWorkStock(new WorkStock
                {
                    Company = lastRediscount.Company,
                    NameTabaco = "REDISCOUNT",
                    TabacoWeigh = details.FinalTabacoWeigh,
                    Data = DateTime.Now
                });
                if (teorFinalTabacoWeigh > details.FinalTabacoWeigh)
                {
                    int disadvantege = teorFinalTabacoWeigh - details.FinalTabacoWeigh;
                    int disHookah = disadvantege / details.Gramovka;
                    return View("ResultRediscount", new ViewResultRediscount {UsedTabaco = useTabaco,
                    Disadvantage = disadvantege, DisadvantageHookah = disHookah} );

                }
                else
                {
                    int excess = details.FinalTabacoWeigh - teorFinalTabacoWeigh;
                    return View("ResultRediscount", new ViewResultRediscount { UsedTabaco = useTabaco, Excess = excess });
                }

            }
            else
            {
                int sumTabacoWeigh = workStockRepository.DisplayWorkStock(userCompanyRepository.CompanyToUser(user.Id)).Sum(
                    p => p.TabacoWeigh);
                int useTabaco = details.CountHookah * details.Gramovka;
                workStockRepository.AddWorkStock(new WorkStock
                {
                    Company = company,
                    NameTabaco = "REDISCOUNT",
                    TabacoWeigh = details.FinalTabacoWeigh,
                    Data = DateTime.Now
                });
                int teorFinalTabacoWeigh = sumTabacoWeigh - useTabaco;

                if (teorFinalTabacoWeigh > details.FinalTabacoWeigh)
                {
                    int disadvantege = teorFinalTabacoWeigh - details.FinalTabacoWeigh;
                    int disHookah = disadvantege / details.Gramovka;
                    return View("ResultRediscount", new ViewResultRediscount
                    {
                        UsedTabaco = useTabaco,
                        Disadvantage = disadvantege,
                        DisadvantageHookah = disHookah
                    });

                }
                else
                {
                    int excess = details.FinalTabacoWeigh - teorFinalTabacoWeigh;
                    return View("ResultRediscount", new ViewResultRediscount { UsedTabaco = useTabaco, Excess = excess });
                }
            }
        }

    }
}
