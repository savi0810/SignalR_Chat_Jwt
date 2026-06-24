using AuthSefviceApi1.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace AuthSefviceApi1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
    }
}
