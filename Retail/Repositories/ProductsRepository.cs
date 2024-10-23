using Microsoft.EntityFrameworkCore;
using Retail.Context;
using Retail.Model;

namespace Retail.Repositories
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Products>> GetProducts();
        Task<Products> GetProductsById(int idProducts);
        Task CreateProducts(string productName, string description, DateTime creationDate, int active, string model3D, int categoryId);
        Task UpdateProducts(int idProducts, string productName, string description, DateTime creationDate, int active, string model3D, int categoryId);
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
                .Include(c => c.Categories)
                .ToListAsync();
        }

        public async Task<Products> GetProductsById(int idProducts)
        {
            return await _dbContext.Products
                .Include(c => c.Categories)
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

        public async Task CreateProducts(string productName, string description, DateTime creationDate, int active, string model3D, int categoryId)
        {
            var category = await _dbContext.Categories.FindAsync(categoryId) ?? throw new Exception("Category not found");


            var product = new Products
            {
                ProductName = productName,
                Description = description,
                CreationDate = creationDate, 
                Active = active,
                Model3D = model3D,
                Categories = category
            };

            try
            {
                await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateProducts(int idProducts, string productName, string description, DateTime creationDate, int active, string model3D, int categoryId)
        {
            var product = await _dbContext.Products.FindAsync(idProducts) ?? throw new Exception("Product not found");

            var category = await _dbContext.Categories.FindAsync(categoryId) ?? throw new Exception("Caregory not found");

            // Update
            product.ProductName = productName;
            product.Description = description;
            product.CreationDate = creationDate;
            product.Active = active;
            product.Model3D = model3D;
            product.Categories = category;

            try
            {
                _dbContext.Products.Update(product);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;

            }
        }
    }

}
