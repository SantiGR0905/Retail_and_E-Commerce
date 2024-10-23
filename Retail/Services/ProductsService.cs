using Retail.Model;
using Retail.Repositories;

namespace Retail.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<Products>> GetProduct();
        Task<Products> GetProductById(int idProduct);
        Task CreateProduct(string productName, string description, DateTime creationDate, int active, string model3D, int categoryId);
        Task UpdateProduct(int idProducts, string productName, string description, DateTime creationDate, int active, string model3D, int categoryId);
        Task SoftDeleteProduct(int idProduct);
    }

    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productRepository;
        public ProductsService(IProductsRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Products>> GetProduct()
        {
            return await _productRepository.GetProducts();
        }

        public async Task<Products> GetProductById(int idProduct)
        {
            return await _productRepository.GetProductsById(idProduct);
        }

        public async Task CreateProduct(string productName, string description, DateTime creationDate, int active, string model3D, int categoryId)
        {
            await _productRepository.CreateProducts(productName, description, creationDate, active, model3D, categoryId);
        }

        public async Task UpdateProduct(int idProducts, string productName, string description, DateTime creationDate, int active, string model3D, int categoryId)
        {
            await _productRepository.UpdateProducts(idProducts, productName, description, creationDate, active, model3D, categoryId);
        }

        public async Task SoftDeleteProduct(int idProduct)
        {
            await _productRepository.SoftDeleteProducts(idProduct);
        }
    }
}
