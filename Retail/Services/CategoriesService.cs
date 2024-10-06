using Retail.Model;
using Retail.Repositories;

namespace Retail.Services
{
    public interface ICategoriesService
    {
        Task<IEnumerable<Categories>> GetCategory();
        Task<Categories> GetCategoryById(int idCategory);
        Task CreateCategory(Categories Category);
        Task UpdateCategory(Categories Category);
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

        public async Task CreateCategory(Categories Category)
        {
            await _CategoriesRepository.CreateCategory(Category);
        }
        public async Task UpdateCategory(Categories Category)
        {
            await _CategoriesRepository.UpdateCategory(Category);
        }
        public async Task SoftDeleteCategory(int idCategory)
        {
            await _CategoriesRepository.SoftDeleteCategory(idCategory);
        }
    }
}
