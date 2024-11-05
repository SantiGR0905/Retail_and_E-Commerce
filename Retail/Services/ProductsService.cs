using Retail.Model;
using Retail.Repositories;

namespace Retail.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<Products>> GetProduct();
        Task<Products> GetProductById(int idProduct);
        Task CreateProduct(string productName, string description, decimal price, bool active, string image, int stock, int categoryId);
        Task UpdateProduct(int idProducts, string productName, string description, DateTime creationDate, decimal price, bool active, string image, int stock, int categoryId);
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

        public async Task CreateProduct(string productName, string description, decimal price, bool active, string image, int stock, int categoryId)
        {
            await _productRepository.CreateProducts(productName, description, price, active, image, stock, categoryId);
        }

        public async Task UpdateProduct(int idProducts, string productName, string description, DateTime creationDate, decimal price, bool active, string image, int stock, int categoryId)
        {
            await _productRepository.UpdateProducts(idProducts, productName, description, creationDate, price, active, image, stock, categoryId);
        }

        public async Task SoftDeleteProduct(int idProduct)
        {
            await _productRepository.SoftDeleteProducts(idProduct);
        }
    }
}
