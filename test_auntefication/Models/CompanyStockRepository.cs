using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test_auntefication.Data;

namespace test_auntefication.Models
{
    public class CompanyStockRepository: ICompanyStockRepository
    {
        private ProductsDbContext productsDb;
        public CompanyStockRepository(ProductsDbContext cxt)
        {
            productsDb = cxt;
        }
        public IQueryable<CompanyStock> CompanyStock => productsDb.CompanyStock;
        public void AddCompStock(CompanyStock company)
        {
            if (company != null)
            {
                productsDb.CompanyStock.Add(company);
            }
            productsDb.SaveChanges();
        }
        public void AddCompStocks(IEnumerable<CompanyStock> companyStocks)
        {
            if (companyStocks != null)
            {
                productsDb.CompanyStock.AddRange(companyStocks);
            }
            productsDb.SaveChanges();
        }
        public void EditCompanyStock(CompanyStock companyStock)
        {
            productsDb.Entry(companyStock).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            productsDb.SaveChanges();
        }
        public List<CompanyStock> DisplayCompanyStock(Company company)
        {
            var result = productsDb.CompanyStock.Where(p => p.CompanyId == company.Id).ToList();
            return result;
        }
    }
}
