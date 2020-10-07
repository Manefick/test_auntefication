using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_auntefication.Models
{
    public interface ICompanyStockRepository
    {
        IQueryable<CompanyStock> CompanyStock { get; }
        void AddCompStock(CompanyStock company);
        void AddCompStocks(IEnumerable<CompanyStock> companyStocks);
        List<CompanyStock> DisplayCompanyStock(Company company);
        void EditCompanyStock(CompanyStock companyStock);


    }
}
