using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test_auntefication.Data;

namespace test_auntefication.Models
{
    public class TabacoRepository: ITabacosRepository
    {
        private ProductsDbContext _context;

        public TabacoRepository(ProductsDbContext context)
        {
            _context = context;
        }
        public IQueryable<Tabaco> Tabacos => _context.Tabaco;

        public void AddTabaco(Tabaco tabac)
        {
            if(tabac != null)
            {
                _context.Tabaco.Add(tabac);
            }

            _context.SaveChanges();
        }
    }
}
