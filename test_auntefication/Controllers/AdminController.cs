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
        private ITabacosRepository tabacosRepository;
        private ICompanyRepository companyRepository;
        private ICompanyStockRepository companyStockRepository;
        private IWorkStockRepository workStockRepository;
        private IUserCompanyRepository userCompanyRepository;
        public AdminController(UserManager<AppUser> usrMng, RoleManager<IdentityRole> roleMng, ITabacosRepository tabacos, 
            ICompanyRepository cr, ICompanyStockRepository csr, IWorkStockRepository wsr, IUserCompanyRepository ucr)
        {
            userManager = usrMng;
            roleManager = roleMng;
            tabacosRepository = tabacos;
            companyRepository = cr;
            companyStockRepository = csr;
            workStockRepository = wsr;
            userCompanyRepository = ucr;
        }
        
        [Authorize(Roles ="Admin")]
        public ViewResult AddTabacos() => View();

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public IActionResult AddTabacos(string Name)
        {
            Tabaco taba = new Tabaco { Name = Name };
            tabacosRepository.AddTabaco(taba);
            return View("TabacoList", tabacosRepository.Tabacos);
        }
        [Authorize(Roles = "Admin")]
        public ViewResult RegistCompany() => View();

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> RegistCompany(ViewRegistrCompany details)
        {
            Company company = new Company { Name = details.CompanyName };
            companyRepository.Add(company);

            CompanyStock companyStock = new CompanyStock
            {
                Company = company,
                TabacoName = details.TabacoNameSt,
                TabacoBundleWeigh = details.TabacoBundleWeithSt,
                TabacoCount = details.TabacoCountSt
            };
            companyStockRepository.AddCompStock(companyStock);

            WorkStock workStock = new WorkStock
            {
                Company = company,
                NameTabaco = details.WSTabacoName,
                TabacoWeigh = details.WSTabacoWeigth
            };
            workStockRepository.AddWorkStock(workStock);
            AppUser appUser = await userManager.FindByNameAsync(details.UserName);
            if(appUser != null)
            {
                UserCompany userCompany = new UserCompany { Company = company, UserId = appUser.Id };
                userCompanyRepository.AddUserCompany(userCompany);
            }
            return RedirectToAction("RegisterUser","Account");
        }
    }
}
