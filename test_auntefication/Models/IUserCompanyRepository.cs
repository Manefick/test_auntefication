using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_auntefication.Models
{
    public interface IUserCompanyRepository
    {
        IQueryable<UserCompany> UserCompany { get; }
        void AddUserCompany(UserCompany userCompany);
    }
}
