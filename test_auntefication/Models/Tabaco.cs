using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_auntefication.Models
{
    public class Tabaco
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CompanyStock> CompanyStocks { get; set; }
        public ICollection<WorkStock> WorkStocks { get; set; }
    }
}
