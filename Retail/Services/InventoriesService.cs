using Retail.Model;
using Retail.Repositories;

namespace Retail.Services
{
    public interface IInventoriesService
    {
        Task<IEnumerable<Inventories>> GetInventory();
        Task<Inventories> GetInventoryById(int idInventory);
        Task CreateInventory(Inventories inventory);
        Task UpdateInventory(Inventories inventory);
        Task SoftDeleteInventory(int idInventory);
    }

    public class InventoriesService : IInventoriesService
    {
        private readonly IInventoriesRepository _inventoriesRepository;
        public InventoriesService(IInventoriesRepository inventoriesRepository)
        {
            _inventoriesRepository = inventoriesRepository;
        }

        public async Task<IEnumerable<Inventories>> GetInventory()
        {
            return await _inventoriesRepository.GetInventory();
        }

        public async Task<Inventories> GetInventoryById(int idInventory)
        {
            return await _inventoriesRepository.GetInventoryById(idInventory);
        }

        public async Task CreateInventory(Inventories inventory)
        {
            await _inventoriesRepository.CreateInventory(inventory);
        }

        public async Task UpdateInventory(Inventories inventory)
        {
            await _inventoriesRepository.UpdateInventory(inventory);
        }

        public async Task SoftDeleteInventory(int idInventory)
        {
            await _inventoriesRepository.SoftDeleteInventory(idInventory);
        }
    }
}
