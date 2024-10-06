using Microsoft.EntityFrameworkCore;
using Retail.Context;
using Retail.Model;

namespace Retail.Repositories
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Products>> GetProducts();
        Task<Products> GetProductsById(int idProducts);
        Task CreateProducts(Products Products);
        Task UpdateProducts(Products Products);
        Task SoftDeleteProducts(int idProducts);
    }

    public class ProductsRepository : IProductsRepository
    {
        private readonly RetailDbContext _dbContext;

        public ProductsRepository(RetailDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Products>> GetProducts()
        {
            return await _dbContext.Products
                .Where(s => !s.IsDeleted)
                .ToListAsync();
        }

        public async Task<Products> GetProductsById(int idProducts)
        {
            return await _dbContext.Products
                .FirstOrDefaultAsync(s => s.ProductId == idProducts && !s.IsDeleted);
        }

        public async Task SoftDeleteProducts(int idProducts)
        {
            var Products = await _dbContext.Products.FindAsync(idProducts);
            if (Products != null)
            {
                Products.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task CreateProducts(Products Products)
        {
            _dbContext.Products.Add(Products);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProducts(Products Products)
        {
            _dbContext.Products.Update(Products);
            await _dbContext.SaveChangesAsync();
        }
    }

}
