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
    }
}
