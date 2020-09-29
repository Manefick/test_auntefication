using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_auntefication.Models
{
    interface IWorkStockRepository
    {
        IQueryable<WorkStock> WorkStock { get; }
    }
}
