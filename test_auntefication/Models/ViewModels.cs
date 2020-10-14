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
        public IEnumerable<ViewCompanyStock> CompanyStock { get; set; }
        public int TabacoWeigth { get; set; }
        public int CountTabacoPack { get; set; }
        public int SelectedCompanyStock { get; set; }
    }
    public class ViewCompanyStock
    {
        public int Id { get; set; }
        public string TabacoName { get; set; }
        public int TabacoBundleWeigh { get; set; }
        public int TabacoCount { get; set; }
        public int CompanyId { get; set; }
        public string Info => $"{TabacoName}  {TabacoBundleWeigh} Остаток на складе:{TabacoCount}";

    }
    public class ViewRedisCount
    {
        public int Gramovka { get; set; }
        public int CountHookah { get; set; }
        public int FinalTabacoWeigh { get; set; }
    }
    public class ViewResultRediscount
    {
        public int UsedTabaco { get; set; }
        public int Disadvantage { get; set; }
        public int Excess { get; set; }
        public int DisadvantageHookah { get; set; }
    }
}
