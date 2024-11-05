using Retail.Model;
using Retail.Repositories;

namespace Retail.Services
{
    public interface ISalesDetailsService
    {
        Task<IEnumerable<SalesDetails>> GetSalesDetails();
        Task<SalesDetails> GetSaleDetailById(int idSaleDetail);
        Task CreateSaleDetail(int quantity, decimal unitPrice, int saleId, int productId);
        Task UpdateSaleDetail(int idSaleDetail, int quantity, decimal unitPrice, int saleId, int productId);
        Task SoftDeleteSaleDetail(int idSaleDetail);
    }
    public class SalesDetailsService : ISalesDetailsService
    {
        private readonly ISalesDetailsRepository _salesDetailsRepository;
        public SalesDetailsService(ISalesDetailsRepository salesDetailsRepository)
        {
            _salesDetailsRepository = salesDetailsRepository;
        }

        public async Task<IEnumerable<SalesDetails>> GetSalesDetails()
        {
            return await _salesDetailsRepository.GetSalesDetails();
        }

        public async Task<SalesDetails> GetSaleDetailById(int idSaleDetail)
        {
            return await _salesDetailsRepository.GetSaleDetailById(idSaleDetail);
        }

        public async Task CreateSaleDetail(int quantity, decimal unitPrice, int saleId, int productId)
        {
            await _salesDetailsRepository.CreateSaleDetail(quantity, unitPrice, saleId, productId);
        }

        public async Task UpdateSaleDetail(int idSaleDetail, int quantity, decimal unitPrice, int saleId, int productId)
        {
            await _salesDetailsRepository.UpdateSaleDetail(idSaleDetail, quantity, unitPrice, saleId, productId);
        }

        public async Task SoftDeleteSaleDetail(int idSaleDetail)
        {
            await _salesDetailsRepository.SoftDeleteSaleDetail(idSaleDetail);
        }
    }
}
