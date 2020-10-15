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
        public void AddWorkStock(WorkStock workStock)
        {
            if (workStock != null)
            {
                context.WorkStock.Add(workStock);
            }
            context.SaveChanges();
        }
        public List<WorkStock> DisplayWorkStock(Company company)
        {

            var result = context.WorkStock.Where(p => p.Company == company).ToList();
            return result;
        }
        public void EditWorkStock(WorkStock workStock)
        {
            context.Entry(workStock).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
