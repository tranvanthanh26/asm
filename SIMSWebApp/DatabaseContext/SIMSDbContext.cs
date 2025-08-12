using Microsoft.EntityFrameworkCore;
using SIMSWebApp.DatabaseContext.Entities;

namespace SIMSWebApp.DatabaseContext
{
    public class SIMSDbContext : DbContext
    {
        public SIMSDbContext(DbContextOptions<SIMSDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // dinh nghia Entity User la bang du lieu Users trong database
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey("UserID");
            modelBuilder.Entity<User>().HasIndex("Username").IsUnique();
            modelBuilder.Entity<User>().Property(u => u.Role).HasDefaultValue("Admin");
        }

    }
}
