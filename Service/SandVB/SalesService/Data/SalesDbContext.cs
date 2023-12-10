using Microsoft.EntityFrameworkCore;
using SalesService.Model;
using System.Collections.Generic;

namespace SalesService.Data
{
    public class SalesDbContext : DbContext
    {
        public DbSet<Sale> Sales { get; set; }
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {

        }
    }
}