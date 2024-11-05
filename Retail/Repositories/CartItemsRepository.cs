using Microsoft.EntityFrameworkCore;
using Retail.Context;
using Retail.Model;
using System;

namespace Retail.Repositories
{
    public interface ICartItemsRepository
    {
        Task<IEnumerable<CartItems>> GetCartItems();
        Task<CartItems> GetCartItemById(int idcartItem);
        Task CreateCartItem(int quantity, int cartId, int productId);
        Task UpdateCartItem(int idcartItem, int quantity, int cartId, int productId);
        Task SoftDeleteCartItem(int idcartItem);
    }
    public class CartItemsRepository : ICartItemsRepository
    {
        private readonly RetailDbContext _dbContext;

        public CartItemsRepository(RetailDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<CartItems>> GetCartItems()
        {
            return await _dbContext.CartItems
                .Where(s => !s.IsDeleted)
                .Include(c => c.Carts)
                .Include(p => p.Products)
                .ToListAsync();
        }

        public async Task<CartItems> GetCartItemById(int idcartItem)
        {
            return await _dbContext.CartItems
                .Include(c => c.Carts)
                .Include(p => p.Products)
                .FirstOrDefaultAsync(c => c.CartItemId == idcartItem && !c.IsDeleted);
        }
        public async Task SoftDeleteCartItem(int idcartItem)
        {
            var cartItem = await _dbContext.CartItems.FindAsync(idcartItem);
            if (cartItem != null)
            {
                cartItem.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task CreateCartItem(int quantity, int cartId, int productId)
        {
            var cart = await _dbContext.Carts.FindAsync(cartId) ?? throw new Exception("Cart not found");
            var product = await _dbContext.Products.FindAsync(productId) ?? throw new Exception("Product not found");

            var cartItem = new CartItems
            {
                Quantity = quantity,
                Carts = cart,
                Products = product
            };

            try
            {
                await _dbContext.CartItems.AddAsync(cartItem);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateCartItem(int idcartItem, int quantity, int cartId, int productId)
        {
            var cartItem = await _dbContext.CartItems.FindAsync(idcartItem) ?? throw new Exception("CartItem not found");

            var cart = await _dbContext.Carts.FindAsync(cartId) ?? throw new Exception("Cart not found");
            var product = await _dbContext.Products.FindAsync(productId) ?? throw new Exception("Product not found");

            // Update
            cartItem.Quantity = quantity;
            cartItem.Carts = cart;
            cartItem.Products  = product;

            try
            {
                _dbContext.CartItems.Update(cartItem);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;

            }
        }
    }
}
