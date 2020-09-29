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
    }
}
