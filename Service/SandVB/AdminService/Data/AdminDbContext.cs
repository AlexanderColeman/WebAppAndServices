using AdminService.Model;
using Microsoft.EntityFrameworkCore;

namespace AdminService.Data
{
    public class AdminDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options)
        {
                
        }
    }
}
