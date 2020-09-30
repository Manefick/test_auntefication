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
        [Authorize]
        public async Task<IActionResult> Index()
        {
            Dictionary<string, Dictionary<string, string>> InfoUser = new Dictionary<string, Dictionary<string, string>>();
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, "Admin"))
                {
                    InfoUser[user.Id] = new Dictionary<string, string> {
                            { "Name", user.UserName },{"Email" ,user.Email }, {"Id", user.Id}, { "Role", "Admin" } };
                }
                else if(await userManager.IsInRoleAsync(user, "User"))
                {
                    InfoUser[user.Id] = new Dictionary<string, string> {
                            { "Name", user.UserName },{"Email" ,user.Email }, {"Id", user.Id}, { "Role", "User" } };
                }
            }

            return View(InfoUser);
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
            AppUser appUser = await userManager.FindByNameAsync(details.UserName);
            if(appUser != null)
            {
                //почему у меня id передаеться в формате строки и нужно ли в моделях переформатировать id в строку
                UserCompany userCompany = new UserCompany { Company = company, UserId = Convert.ToInt32(appUser.Id) };
            }
            return View("Index");
        }
    }
}
