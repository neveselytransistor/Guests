using Guests.Models;
using Microsoft.EntityFrameworkCore;

namespace Guests
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=data.db");
        }

        public DbSet<UserInfo> Users { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }
    }
}