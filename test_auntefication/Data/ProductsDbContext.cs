using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using test_auntefication.Models;

namespace test_auntefication.Data
{
    public class ProductsDbContext: DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options) { }
        public DbSet<Tabaco> Tabaco { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyStock> CompanyStock { get; set; }
        public DbSet<WorkStock> WorkStock { get; set; }
    }
}
