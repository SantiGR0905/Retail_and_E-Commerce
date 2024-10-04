using Microsoft.EntityFrameworkCore;
using Retail.Model;

namespace Retail.Context
{
    public class RetailDbContext : DbContext
    {
        public RetailDbContext(DbContextOptions options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>()
                .HasKey(u => u.UserId);
        }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Inventories> Inventories { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<PermissionsXUsers> PermissionsXUsers { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Users> Users {  get; set; }
        public DbSet<UserTypes> UserTypes { get; set; }
    }
}
