using Microsoft.EntityFrameworkCore;
using Retail.Context;
using Retail.Model;

namespace Retail.Repositories
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Categories>> GetCategory();
        Task<Categories> GetCategoryById(int idCategory);
        Task CreateCategory(string categoryName, string categoryDescription);
        Task UpdateCategory(int idCategory, string categoryName, string categoryDescription);
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

        public async Task CreateCategory(string categoryName, string categoryDescription)
        {
            var category = new Categories
            {
                CategoryName = categoryName,
                CategoryDescription = categoryDescription
            };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCategory(int idCategory, string categoryName, string categoryDescription)
        {
            var category = await _dbContext.Categories.FindAsync(idCategory) ?? throw new Exception("Category not found");

            category.CategoryName = categoryName;
            category.CategoryDescription = categoryDescription;

            try
            {
                _dbContext.Categories.Update(category);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception e)
            {

                throw;

            }
        }
    }
}
