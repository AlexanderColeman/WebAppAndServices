using AdminService.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminService.Data
{
    public class AdminDbContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options)
        {
                
        }
    }
}
