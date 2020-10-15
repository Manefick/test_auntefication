using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_auntefication.Models
{
    public interface IWorkStockRepository
    {
        IQueryable<WorkStock> WorkStock { get; }
        void AddWorkStock(WorkStock workStock);
        void EditWorkStock(WorkStock workStock);
        List<WorkStock> DisplayWorkStock(Company company);
    }
}
