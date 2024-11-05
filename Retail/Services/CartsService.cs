using Retail.Model;
using Retail.Repositories;

namespace Retail.Services
{
    public interface ICartsService
    {
        Task<IEnumerable<Carts>> GetCarts();
        Task<Carts> GetCartById(int idcart);
        Task CreateCart(bool isActive, int userId);
        Task UpdateCart(int idcart, DateTime created, bool isActive, int userId);
        Task SoftDeleteCart(int idcart);
    }
    public class CartsService : ICartsRepository
    {
        private readonly ICartsRepository _cartsRepository;
        public CartsService(ICartsRepository cartsRepository)
        {
            _cartsRepository = cartsRepository;
        }
        public async Task<IEnumerable<Carts>> GetCarts()
        {
            return await _cartsRepository.GetCarts();
        }
        public async Task<Carts> GetCartById(int idcart)
        {
            return await _cartsRepository.GetCartById(idcart);
        }

        public async Task CreateCart(bool isActive, int userId)
        {
            await _cartsRepository.CreateCart(isActive, userId);
        }
        public async Task UpdateCart(int idcart, DateTime created, bool isActive, int userId)
        {
            await _cartsRepository.UpdateCart(idcart, created, isActive, userId);
        }
        public async Task SoftDeleteCart(int idcart)
        {
            await _cartsRepository.SoftDeleteCart(idcart);
        }
    }
}
