using Microsoft.EntityFrameworkCore;
using Retail.Context;
using Retail.Model;
using System;

namespace Retail.Repositories
{
    public interface ICartsRepository
    {
        Task<IEnumerable<Carts>> GetCarts();
        Task<Carts> GetCartById(int idcart);
        Task CreateCart(bool isActive, int userId);
        Task UpdateCart(int idcart, DateTime created, bool isActive, int userId);
        Task SoftDeleteCart(int idcart);
    }
    public class CartsRepository : ICartsRepository
    {
        private readonly RetailDbContext _dbContext;

        public CartsRepository(RetailDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Carts>> GetCarts()
        {
            return await _dbContext.Carts
                .Where(s => !s.IsDeleted)
                .Include(u => u.Users)
                .ToListAsync();
        }

        public async Task<Carts> GetCartById(int idcart)
        {
            return await _dbContext.Carts
                .Include(u => u.Users)
                .FirstOrDefaultAsync(c => c.CartId == idcart && !c.IsDeleted);
        }
        public async Task SoftDeleteCart(int idcart)
        {
            var cart = await _dbContext.Carts.FindAsync(idcart);
            if (cart != null)
            {
                cart.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task CreateCart(bool isActive, int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId) ?? throw new Exception("User not found");


            var cart = new Carts
            {
                Created = DateTime.Now,
                IsActive = isActive,
                Users = user,
            };

            try
            {
                await _dbContext.Carts.AddAsync(cart);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateCart(int idcart, DateTime created, bool isActive, int userId)
        {
            var cart = await _dbContext.Carts.FindAsync(idcart) ?? throw new Exception("Cart not found");

            var user = await _dbContext.Users.FindAsync(userId) ?? throw new Exception("User not found");

            // Update
            cart.Created = created;
            cart.IsActive = isActive;
            cart.Users = user;

            try
            {
                _dbContext.Carts.Update(cart);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;

            }
        }
    }
}
