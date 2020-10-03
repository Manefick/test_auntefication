using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test_auntefication.Data;

namespace test_auntefication.Models
{
    public class UserCompanyRepository:IUserCompanyRepository
    {
        private ProductsDbContext context;
        public UserCompanyRepository(ProductsDbContext cnt)
        {
            context = cnt;
        }
        public IQueryable<UserCompany> UserCompany => context.UserCompany;
        public void AddUserCompany(UserCompany userCompany)
        {
            if (userCompany != null)
            {
                context.UserCompany.Add(userCompany);
            }
            context.SaveChanges();
        }
        public void AddUserToCompany(string UserId)
        {
            
            var res = context.UserCompany.Where(p => p.UserId == UserId).Select(userCompany=>userCompany.Company).ToList();
            //UserCompany newUser = new UserCompany { Company = res.FirstOrDefault().Company, UserId = user.Id };
            if(res != null)
            {
                UserCompany userCompanies = new UserCompany { Company = res.FirstOrDefault(), UserId = UserId };
                context.UserCompany.Add(userCompanies);
            }
            context.SaveChanges();
        }
        
    }
}
 