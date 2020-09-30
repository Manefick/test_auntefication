using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_auntefication.Models
{
    public class WorkStock
    {
        public int Id { get; set; }
        public string NameTabaco { get; set; }
        public int TabacoWeigh { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
