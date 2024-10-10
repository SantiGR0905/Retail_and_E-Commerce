using Retail.Model;
using Retail.Repositories;

namespace Retail.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<Products>> GetProduct();
        Task<Products> GetProductById(int idProduct);
        Task CreateProduct(Products product);
        Task UpdateProduct(Products product);
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

        public async Task CreateProduct(Products product)
        {
            await _productRepository.CreateProducts(product);
        }

        public async Task UpdateProduct(Products product)
        {
            await _productRepository.UpdateProducts(product);
        }

        public async Task SoftDeleteProduct(int idProduct)
        {
            await _productRepository.SoftDeleteProducts(idProduct);
        }
    }
}
