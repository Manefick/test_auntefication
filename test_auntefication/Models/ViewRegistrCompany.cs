using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_auntefication.Models
{
    public class ViewRegistrCompany
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public string TabacoNameSt { get; set; }
        public int TabacoBundleWeithSt { get; set; }
        public int TabacoCountSt { get; set; }
        public string WSTabacoName { get; set; }
        public int WSTabacoWeigth { get; set; }

    }
    public class ViewAddTabacoToStock
    {
        public string TabacoName { get; set; }
        public int TabacoBundleWeigh { get; set; }
        public int TabacoCount { get; set; }
    }
    public class ViewAddTabacoToStockList
    {
        public List<ViewAddTabacoToStock> tabacoToStocks { get; set; }
    }
    public class AddTabacoToWorkStock
    {
        public IEnumerable<CompanyStock> CompanyStock { get; set; }
        public int TabacoWeigth { get; set; }
        public int CountTabacoPack { get; set; }
        public int SelectedCompanyStock { get; set; }
    }
}
