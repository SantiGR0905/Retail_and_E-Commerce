using Retail.Model;
using Retail.Repositories;

namespace Retail.Services
{
    public interface ICategoriesService
    {
        Task<IEnumerable<Categories>> GetCategory();
        Task<Categories> GetCategoryById(int idCategory);
        Task CreateCategory(string categoryName, string categoryDescription);
        Task UpdateCategory(int idCategory, string categoryName, string categoryDescription);
        Task SoftDeleteCategory(int idCategory);
    }
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _CategoriesRepository;
        public CategoriesService(ICategoriesRepository CategoriesRepository)
        {
            _CategoriesRepository = CategoriesRepository;
        }
        public async Task<IEnumerable<Categories>> GetCategory()
        {
            return await _CategoriesRepository.GetCategory();
        }
        public async Task<Categories> GetCategoryById(int idCategory)
        {
            return await _CategoriesRepository.GetCategoryById(idCategory);
        }

        public async Task CreateCategory(string categoryName, string categoryDescription)
        {
            await _CategoriesRepository.CreateCategory(categoryName, categoryDescription);
        }
        public async Task UpdateCategory(int idCategory, string categoryName, string categoryDescription)
        {
            await _CategoriesRepository.UpdateCategory(idCategory, categoryName, categoryDescription);
        }
        public async Task SoftDeleteCategory(int idCategory)
        {
            await _CategoriesRepository.SoftDeleteCategory(idCategory);
        }
    }
}
