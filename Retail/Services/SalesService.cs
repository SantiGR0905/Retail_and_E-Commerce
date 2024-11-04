using Retail.Model;
using Retail.Repositories;

namespace Retail.Services
{
    public interface ISalesService
    {
        Task<IEnumerable<Sales>> GetSales();
        Task<Sales> GetSalesById(int idsale);
        Task CreateSales(int stateSale, string direction, int userId, int productId);
        Task UpdateSales(int idsales, DateTime saleDate, int stateSale, string direction, int userId, int productId);
        Task SoftDeleteSales(int idsale);
    }
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;
        public SalesService(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }
        public async Task<IEnumerable<Sales>> GetSales()
        {
            return await _salesRepository.GetSales();
        }
        public async Task<Sales> GetSalesById(int idsale)
        {
            return await _salesRepository.GetSalesById(idsale);
        }

        public async Task CreateSales(int stateSale, string direction, int userId, int productId)
        {
            await _salesRepository.CreateSales(stateSale, direction, userId, productId);
        }
        public async Task UpdateSales(int idsales, DateTime saleDate, int stateSale, string direction, int userId, int productId)
        {
            await _salesRepository.UpdateSales(idsales, saleDate, stateSale, direction, userId, productId);
        }
        public async Task SoftDeleteSales(int idsale)
        {
            await _salesRepository.SoftDeleteSales(idsale);
        }
    }
}
