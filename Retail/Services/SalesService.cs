using Retail.Model;
using Retail.Repositories;

namespace Retail.Services
{
    public interface ISalesService
    {
        Task<IEnumerable<Sales>> GetSales();
        Task<Sales> GetSalesById(int idsale);
        Task CreateSales(Sales sales);
        Task UpdateSales(Sales sales);
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

        public async Task CreateSales(Sales sales)
        {
            await _salesRepository.CreateSales(sales);
        }
        public async Task UpdateSales(Sales sales)
        {
            await _salesRepository.UpdateSales(sales);
        }
        public async Task SoftDeleteSales(int idsale)
        {
            await _salesRepository.SoftDeleteSales(idsale);
        }
    }
}
