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
                TabacoWeigh = details.WSTabacoWeigth,
                HookahMaster= User.Identity.Name
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
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> EditWorkStock()
        {//доработать віпадающий список с именем табака и фасовкой добавить в модель табака поле фосовки
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            Company company = userCompanyRepository.CompanyToUser(user.Id);
            IEnumerable<Tabaco> tabacos = tabacosRepository.Tabacos.ToList();
            List<ViewTabaco> viewTabacos = new List<ViewTabaco>();
            if (tabacos !=null)
            {
                foreach(Tabaco tb in tabacos)
                {
                    viewTabacos.Add(new ViewTabaco { Id = tb.Id, Name = tb.Name, TabacoBundleWeige = tb.NominalWeigth });
                }
            }
            IEnumerable<WorkStock> workStock = workStockRepository.DisplayWorkStock(company).Where(p => p.NameTabaco != "REDISCOUNT");
            List<ViewWorkStock> workStocksList = new List<ViewWorkStock>();
            if (workStock != null)
            {
                foreach(WorkStock ws in workStock)
                {
                    workStocksList.Add(new ViewWorkStock
                    {
                        TabacoName = ws.NameTabaco,
                        Id = ws.Id,
                        CompanyId = ws.CompanyId,
                        Data = ws.Data,
                        TabacoWeigh = ws.TabacoWeigh,
                        CountTabacoPack = ws.CountTabacoPack,
                        HookahMaster = ws.HookahMaster
                    });
                }
            }
            return View(new EditWorkStockView {workStocks = workStocksList, Tabacos = viewTabacos });
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> EditWorkStock(EditWorkStockView details)
        {
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            Company companyUser = userCompanyRepository.CompanyToUser(user.Id);
            WorkStock workStock = workStockRepository.DisplayWorkStock(companyUser).Where(p => p.Id == details.SelectedWorkStock).
                FirstOrDefault();
            Tabaco tabaco = tabacosRepository.Tabacos.Where(x => x.Id == details.TabacoId).FirstOrDefault();
            CompanyStock companyStock = companyStockRepository.DisplayCompanyStock(companyUser)
                .Where(p => string.Equals(p.TabacoName,workStock.NameTabaco)&&p.TabacoBundleWeigh==workStock.BundleTabacoWeigh)
                .FirstOrDefault();
            CompanyStock newCompanyStock = companyStockRepository.DisplayCompanyStock(companyUser).Where(p =>
            string.Equals(p.TabacoName, tabaco.Name) && p.TabacoBundleWeigh == tabaco.NominalWeigth).FirstOrDefault();
            if (workStock != null&&companyStock!=null&&newCompanyStock!=null)
            {
                workStockRepository.DeleteWorkStock(workStock);
                workStockRepository.AddWorkStock(new WorkStock
                {
                    Company = companyUser,
                    NameTabaco = tabaco.Name,
                    BundleTabacoWeigh = tabaco.NominalWeigth,
                    CountTabacoPack = details.CountTabacoPack,
                    TabacoWeigh = details.TabacoWeigth,
                    Data = DateTime.Now,
                    HookahMaster = User.Identity.Name
                });
                companyStock.TabacoCount = companyStock.TabacoCount + workStock.CountTabacoPack;
                companyStockRepository.EditCompanyStock(companyStock);
                newCompanyStock.TabacoCount -= details.CountTabacoPack;
                companyStockRepository.EditCompanyStock(newCompanyStock);
            }
            return RedirectToAction("ShowWorkStock", "Display");
        }
    }
}
