using Retail.Model;
using Retail.Repositories;

namespace Retail.Services
{
    public interface ICartItemsService
    {
        Task<IEnumerable<CartItems>> GetCartItems();
        Task<CartItems> GetCartItemById(int idcartItem);
        Task CreateCartItem(int quantity, int cartId, int productId);
        Task UpdateCartItem(int idcartItem, int quantity, int cartId, int productId);
        Task SoftDeleteCartItem(int idcartItem);
    }
    public class CartItemsService : ICartItemsService
    {
        private readonly ICartItemsRepository _cartItemsRepository;
        public CartItemsService(ICartItemsRepository cartItemsRepository)
        {
            _cartItemsRepository = cartItemsRepository;
        }
        public async Task<IEnumerable<CartItems>> GetCartItems()
        {
            return await _cartItemsRepository.GetCartItems();
        }
        public async Task<CartItems> GetCartItemById(int idcartItem)
        {
            return await _cartItemsRepository.GetCartItemById(idcartItem);
        }

        public async Task CreateCartItem(int quantity, int cartId, int productId)
        {
            await _cartItemsRepository.CreateCartItem(quantity, cartId, productId);
        }
        public async Task UpdateCartItem(int idcartItem, int quantity, int cartId, int productId)
        {
            await _cartItemsRepository.UpdateCartItem(idcartItem, quantity, cartId, productId);
        }
        public async Task SoftDeleteCartItem(int idcartItem)
        {
            await _cartItemsRepository.SoftDeleteCartItem(idcartItem);
        }
    }
}
