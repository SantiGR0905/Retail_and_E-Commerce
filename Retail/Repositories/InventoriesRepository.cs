using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Retail.Context;
using Retail.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Retail.Repositories
{
    public interface IInventoriesRepository
    {
        Task<IEnumerable<Inventories>> GetInventory();
        Task<Inventories> GetInventoryById(int idInventory);
        Task CreateInventory(int amount);
        Task UpdateInventory(int idInventory, int amount);
        Task SoftDeleteInventory(int idInventory);
    }

    public class InventoriesRepository : IInventoriesRepository
    {
        private readonly RetailDbContext _dbContext;

        public InventoriesRepository(RetailDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Inventories>> GetInventory()
        {
            return await _dbContext.Inventories
                .Where(s => !s.IsDeleted)
                .ToListAsync();
        }

        public async Task<Inventories> GetInventoryById(int idInventory)
        {
            return await _dbContext.Inventories.AsNoTracking()
                .FirstOrDefaultAsync(s => s.InventoryId == idInventory && !s.IsDeleted);
        }

        public async Task SoftDeleteInventory(int idInventory)
        {
            var inventory = await _dbContext.Inventories.FindAsync(idInventory);
            if (inventory != null)
            {
                inventory.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task CreateInventory(int amount)
        {

         
            var inventory = new Inventories
            {
                Amount = amount,
                LastUpdate = DateTime.Now
            };

            try
            {
                await _dbContext.Inventories.AddAsync(inventory);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateInventory(int idInventory, int amount)
        {
            var inventory = await _dbContext.Inventories.FindAsync(idInventory) ?? throw new Exception("Inventory not found");

            // Update
            inventory.Amount = amount;
            inventory.LastUpdate = DateTime.Now;

            try
            {
                _dbContext.Inventories.Update(inventory);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;

            }
        }
    }
}
