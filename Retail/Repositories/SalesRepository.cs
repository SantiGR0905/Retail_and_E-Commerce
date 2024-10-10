using Microsoft.EntityFrameworkCore;
using Retail.Context;
using Retail.Model;

namespace Retail.Repositories
{
    public interface ISalesRepository
    {
        Task<IEnumerable<Sales>> GetSales();
        Task<Sales> GetSalesById(int idsales);
        Task CreateSales(Sales sales);
        Task UpdateSales(Sales sales);
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
                .ToListAsync();
        }

        public async Task<Sales> GetSalesById(int idsales)
        {
            return await _dbContext.Sales
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

        public async Task CreateSales(Sales sales)
        {
            _dbContext.Sales.Add(sales);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSales(Sales sales)
        {
            _dbContext.Sales.Update(sales);
            await _dbContext.SaveChangesAsync();
        }

    }
}
