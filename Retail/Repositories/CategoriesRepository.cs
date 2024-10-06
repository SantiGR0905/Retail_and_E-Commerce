using Microsoft.EntityFrameworkCore;
using Retail.Context;
using Retail.Model;

namespace Retail.Repositories
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Categories>> GetCategory();
        Task<Categories> GetCategoryById(int idCategory);
        Task CreateCategory(Categories Category);
        Task UpdateCategory(Categories Category);
        Task SoftDeleteCategory(int idCategory);
    }
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly RetailDbContext _dbContext;

        public CategoriesRepository(RetailDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Categories>> GetCategory()
        {
            return await _dbContext.Categories
                .Where(s => !s.IsDeleted)
                .ToListAsync();
        }

        public async Task<Categories> GetCategoryById(int idCategory)
        {
            return await _dbContext.Categories
                .FirstOrDefaultAsync(s => s.CategoryId == idCategory && !s.IsDeleted);
        }
        public async Task SoftDeleteCategory(int idCategory)
        {
            var Category = await _dbContext.Categories.FindAsync(idCategory);
            if (Category != null)
            {
                Category.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task CreateCategory(Categories Category)
        {
            _dbContext.Categories.Add(Category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCategory(Categories Category)
        {
            _dbContext.Categories.Update(Category);
            await _dbContext.SaveChangesAsync();
        }
    }
}
