using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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

            modelBuilder.Entity<CartItems>()
                .HasKey(u => u.CartItemId);

            modelBuilder.Entity<Carts>()
                .HasKey(u => u.CartId);

            modelBuilder.Entity<Categories>()
                .HasKey(u => u.CategoryId);

            modelBuilder.Entity<PaymentMethods>()
                .HasKey(u => u.PaymentMethodId);

            modelBuilder.Entity<Permissions>()
               .HasKey(u => u.PermissionId);

            modelBuilder.Entity<PermissionsXUsers>()
              .HasKey(u => u.PermissionXUserId);

            modelBuilder.Entity<Products>()
              .HasKey(u => u.ProductId);

            modelBuilder.Entity<Sales>()
              .HasKey(u => u.SaleId);

            modelBuilder.Entity<SalesDetails>()
                .HasKey(u => u.SaleDetailId);

            modelBuilder.Entity<Users>()
              .HasKey(u => u.UserId);

            modelBuilder.Entity<UserTypes>()
              .HasKey(u => u.UserTypeId);

            modelBuilder.Entity<CartItemHistories>()
                .HasKey(u => u.CartItemHistoryId);

            modelBuilder.Entity<CartHistories>()
                .HasKey(u => u.CartHistoryId);

            modelBuilder.Entity<CategoryHistories>()
                .HasKey(u => u.CategoryHistoryId);

            modelBuilder.Entity<PaymentMethodHistories>()
                .HasKey(u => u.PaymentMethodHistoryId);

            modelBuilder.Entity<ProductHistories>()
                .HasKey(u => u.ProductHistoryId);

            modelBuilder.Entity<SaleHistories>()
                .HasKey(u => u.SaleHistoryId);

            modelBuilder.Entity<SaleDetailHistories>()
                .HasKey(u => u.SaleDetailHistoryId);

            modelBuilder.Entity<UserHistories>()
            .HasKey(u => u.UserHistoryId);

            //modelBuilder.Entity<Users>().ToTable(tb => tb.UseSqlOutputClause(false));
            //modelBuilder.Entity<Products>().ToTable(tb => tb.UseSqlOutputClause(false));
            //modelBuilder.Entity<Sales>().ToTable(tb => tb.UseSqlOutputClause(false));
        }
        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<Carts> Carts { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<PaymentMethods> PaymentMethods { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<PermissionsXUsers> PermissionsXUsers { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<SalesDetails> SalesDetails { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Users> Users {  get; set; }
        public DbSet<UserTypes> UserTypes { get; set; }
        public DbSet<CartItemHistories> CartItemHistories { get; set; }
        public DbSet<CartHistories> CartHistories { get; set; }
        public DbSet<CategoryHistories> CategoryHistories { get; set; }
        public DbSet<PaymentMethodHistories> PaymentMethodHistories { get; set; }
        public DbSet<ProductHistories> ProductHistories { get; set; }
        public DbSet<SaleDetailHistories> SaleDetailHistories { get; set; }
        public DbSet<SaleHistories> SaleHistories { get; set; }
        public DbSet<UserHistories> UserHistories { get; set; }
    }
}
