using Microsoft.EntityFrameworkCore;
using Retail.Context;
using Retail.Model;
using System;

namespace Retail.Repositories
{
    public interface ISalesRepository
    {
        Task<IEnumerable<Sales>> GetSales();
        Task<Sales> GetSalesById(int idsales);
        Task CreateSales(DateTime saleDate, int stateSale, string direction, int userId, int productId);
        Task UpdateSales(int idsales,DateTime saleDate, int stateSale, string direction, int userId, int productId);
        Task SoftDeleteSales(int idsales);
    }
    public class SalesRepository : ISalesRepository
    {
        private readonly RetailDbContext _dbContext;

        public SalesRepository(RetailDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Sales>> GetSales()
        {
            return await _dbContext.Sales
                .Where(s => !s.IsDeleted)
                .Include(u => u.Users)
                .Include(p => p.Products)
                .ToListAsync();
        }

        public async Task<Sales> GetSalesById(int idsales)
        {
            return await _dbContext.Sales
                .Include(u => u.Users)
                .Include(p => p.Products)
                .FirstOrDefaultAsync(s => s.SaleId == idsales && !s.IsDeleted);
        }
        public async Task SoftDeleteSales(int idsales)
        {
            var sales = await _dbContext.Sales.FindAsync(idsales);
            if (sales != null)
            {
                sales.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task CreateSales(DateTime saleDate, int stateSale, string direction, int userId, int productId)
        {
            var user = await _dbContext.Users.FindAsync(userId) ?? throw new Exception("User not found");
            var product = await _dbContext.Products.FindAsync(productId) ?? throw new Exception("Product not found");


            var sale = new Sales
            {
                SaleDate = saleDate,
                StateSale = stateSale,
                Direction = direction,
                Users = user,
                Products = product
            };

            try
            {
                await _dbContext.Sales.AddAsync(sale);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateSales(int idsales, DateTime saleDate, int stateSale, string direction, int userId, int productId)
        {
            var sale = await _dbContext.Sales.FindAsync(idsales) ?? throw new Exception("Sale not found");

            var user = await _dbContext.Users.FindAsync(userId) ?? throw new Exception("User not found");
            var product = await _dbContext.Products.FindAsync(productId) ?? throw new Exception("Product not found");

            // Update
            sale.SaleDate = saleDate;
            sale.StateSale = stateSale;
            sale.Direction = direction;
            sale.Users = user;
            sale.Products = product;

            try
            {
                _dbContext.Sales.Update(sale);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;

            }
        }

    }
}
