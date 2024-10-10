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

            modelBuilder.Entity<UserHistories>()
            .HasKey(u => u.UserHistoryId);

            modelBuilder.Entity<SaleHistories>()
                .HasKey(u => u.SaleHistoryId); 

            modelBuilder.Entity<ProductHistories>()
                .HasKey(u => u.ProductHistoryId);

            modelBuilder.Entity<InventoryHistories>()
                .HasKey(u =>u.InventoryHistoryId);

            modelBuilder.Entity<Users>().ToTable(tb => tb.UseSqlOutputClause(false));
            modelBuilder.Entity<Products>().ToTable(tb => tb.UseSqlOutputClause(false));
            modelBuilder.Entity<Sales>().ToTable(tb => tb.UseSqlOutputClause(false));
            modelBuilder.Entity<Inventories>().ToTable(tb => tb.UseSqlOutputClause(false));
        }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Inventories> Inventories { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<PermissionsXUsers> PermissionsXUsers { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Users> Users {  get; set; }
        public DbSet<UserTypes> UserTypes { get; set; }
        public DbSet<UserHistories> UserHistories { get; set; }
        public DbSet<SaleHistories> SaleHistories { get; set; }
        public DbSet<InventoryHistories> InventoryHistories { get; set; }
        public DbSet<ProductHistories> ProductHistories { get; set; }
    }
}
