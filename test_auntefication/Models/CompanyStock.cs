using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_auntefication.Models
{
    public class CompanyStock
    {
        public int Id { get; set; }
        public string TabacoName { get; set; }
        public int TabacoBundleWeigh { get; set; }
        public int TabacoCount { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
