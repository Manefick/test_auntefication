using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test_auntefication.Data;

namespace test_auntefication.Models
{
    public class EFTabacosRepository: ITabacosRepository
    {
        private ProductsDbContext context;
        public EFTabacosRepository(ProductsDbContext cnx)
        {
            context = cnx;
        }
        public IQueryable<Tabacos> Tabacos => context.Tabacos;

        public void AddTabaco(Tabacos tabac)
        {
            if(tabac != null)
            {
                context.Tabacos.Add(tabac);
            }
            context.SaveChanges();
            
        }
    }
}
