using Retail.Model;
using Retail.Repositories;

namespace Retail.Services
{
    public interface IInventoriesService
    {
        Task<IEnumerable<Inventories>> GetInventory();
        Task<Inventories> GetInventoryById(int idInventory);
        Task CreateInventory(int amount);
        Task UpdateInventory(int idInventory, int amount);
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

        public async Task CreateInventory(int amount)
        {
            await _inventoriesRepository.CreateInventory(amount);
        }

        public async Task UpdateInventory(int idInventory, int amount)
        {
            await _inventoriesRepository.UpdateInventory(idInventory, amount);
        }

        public async Task SoftDeleteInventory(int idInventory)
        {
            await _inventoriesRepository.SoftDeleteInventory(idInventory);
        }
    }
}
