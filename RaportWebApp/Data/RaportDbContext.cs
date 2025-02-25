using Microsoft.EntityFrameworkCore;
using RaportDB.Models;

namespace RaportDB.Data
{
    public class RaportDbContext : DbContext
    {
        public RaportDbContext(DbContextOptions<RaportDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
