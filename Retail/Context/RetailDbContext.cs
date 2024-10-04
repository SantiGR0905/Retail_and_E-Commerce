using Microsoft.EntityFrameworkCore;
using Retail.Model;
using System.Reflection.Metadata;

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

            modelBuilder.Entity<Categories>()
                .HasKey(u => u.CategoryId);

            modelBuilder.Entity<Inventories>()
               .HasKey(u => u.InventoryId);

            modelBuilder.Entity<Permissions>()
               .HasKey(u => u.PermissionId);

            modelBuilder.Entity<PermissionsXUsers>()
              .HasKey(u => u.PermissionXUserId);

            modelBuilder.Entity<Products>()
              .HasKey(u => u.ProductId);

            modelBuilder.Entity<Sales>()
              .HasKey(u => u.SaleId);

            modelBuilder.Entity<Users>()
              .HasKey(u => u.UserId);

            modelBuilder.Entity<UserTypes>()
            .HasKey(u => u.UserTypeId);

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
