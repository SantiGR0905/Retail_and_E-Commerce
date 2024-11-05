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
        Task CreateSales(string stateSale, string direction, int userId, int paymentMethodId);
        Task UpdateSales(int idsales, DateTime saleDate, string stateSale, string direction, int userId, int paymentMethodId);
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
                .Include(p => p.PaymentMethods)
                .ToListAsync();
        }

        public async Task<Sales> GetSalesById(int idsales)
        {
            return await _dbContext.Sales
                .Include(u => u.Users)
                .Include(p => p.PaymentMethods)
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

        public async Task CreateSales(string stateSale, string direction, int userId, int paymentMethodId)
        {
            var user = await _dbContext.Users.FindAsync(userId) ?? throw new Exception("User not found");
            var paymentMethod = await _dbContext.PaymentMethods.FindAsync(paymentMethodId) ?? throw new Exception("Payment Method not found");


            var sale = new Sales
            {
                SaleDate = DateTime.Now,
                StateSale = stateSale,
                Direction = direction,
                Users = user,
                PaymentMethods = paymentMethod,
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

        public async Task UpdateSales(int idsales, DateTime saleDate, string stateSale, string direction, int userId, int paymentMethodId)
        {
            var sale = await _dbContext.Sales.FindAsync(idsales) ?? throw new Exception("Sale not found");

            var user = await _dbContext.Users.FindAsync(userId) ?? throw new Exception("User not found");
            var paymentMethod = await _dbContext.PaymentMethods.FindAsync(paymentMethodId) ?? throw new Exception("Product not found");

            // Update
            sale.SaleDate = saleDate;
            sale.StateSale = stateSale;
            sale.Direction = direction;
            sale.Users = user;
            sale.PaymentMethods = paymentMethod;

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
