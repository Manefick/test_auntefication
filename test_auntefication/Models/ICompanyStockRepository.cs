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
    }
}
