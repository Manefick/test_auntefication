using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test_auntefication.Data;

namespace test_auntefication.Models
{
    public class EFCompanyRepository:ICompanyRepository
    {
        private ProductsDbContext productsDb;
        public EFCompanyRepository(ProductsDbContext cnt)
        {
            productsDb = cnt;
        }
        public IQueryable<Company> Company => productsDb.Company;
        public void Add(Company company)
        {
            productsDb.Company.Add(company);
            productsDb.SaveChanges();
        }
    }
}
