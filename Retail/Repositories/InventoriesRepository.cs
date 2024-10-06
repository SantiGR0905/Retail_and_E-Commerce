using Microsoft.EntityFrameworkCore;
using Retail.Context;
using Retail.Model;

namespace Retail.Repositories
{
    public interface IInventoriesRepository
    {
        Task<IEnumerable<Inventories>> GetInventory();
        Task<Inventories> GetInventoryById(int idInventory);
        Task CreateInventory(Inventories inventory);
        Task UpdateInventory(Inventories inventory);
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
            return await _dbContext.Inventories
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

        public async Task CreateInventory(Inventories inventory)
        {
            _dbContext.Inventories.Add(inventory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateInventory(Inventories inventory)
        {
            _dbContext.Inventories.Update(inventory);
            await _dbContext.SaveChangesAsync();
        }
    }
}
