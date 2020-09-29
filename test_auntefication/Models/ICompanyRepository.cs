using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_auntefication.Models
{
    public interface ICompanyRepository
    {
        IQueryable<Company> Company { get; }
        void Add(Company company);
    }
}
