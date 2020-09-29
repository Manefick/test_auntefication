using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test_auntefication.Data;

namespace test_auntefication.Models
{
    public class WorkStockRepository: IWorkStockRepository
    {
        private ProductsDbContext context;
        public WorkStockRepository(ProductsDbContext cxt)
        {
            context = cxt;
        }
        public IQueryable<WorkStock> WorkStock => context.WorkStock;
    }
}
