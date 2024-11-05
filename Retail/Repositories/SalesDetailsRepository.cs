using Microsoft.EntityFrameworkCore;
using Retail.Context;
using Retail.Model;

namespace Retail.Repositories
{
    public interface ISalesDetailsRepository
    {
        Task<IEnumerable<SalesDetails>> GetSalesDetails();
        Task<SalesDetails> GetSaleDetailById(int idSaleDetail);
        Task CreateSaleDetail(int quantity, decimal unitPrice, int saleId, int productId);
        Task UpdateSaleDetail(int idSaleDetail, int quantity, decimal unitPrice, int saleId, int productId);
        Task SoftDeleteSaleDetail(int idSaleDetail);
    }
    public class SalesDetailsRepository : ISalesDetailsRepository
    {
        private readonly RetailDbContext _dbContext;

        public SalesDetailsRepository(RetailDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<SalesDetails>> GetSalesDetails()
        {
            return await _dbContext.SalesDetails
                .Where(s => !s.IsDeleted)
                .Include(c => c.Sales)
                .Include(p => p.Products)
                .ToListAsync();
        }

        public async Task<SalesDetails> GetSaleDetailById(int idSaleDetail)
        {
            return await _dbContext.SalesDetails
                .Include(c => c.Sales)
                .Include(p => p.Products)
                .FirstOrDefaultAsync(s => s.SaleDetailId == idSaleDetail && !s.IsDeleted);
        }

        public async Task SoftDeleteSaleDetail(int idSaleDetail)
        {
            var SalesDetails = await _dbContext.SalesDetails.FindAsync(idSaleDetail);
            if (SalesDetails != null)
            {
                SalesDetails.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task CreateSaleDetail(int quantity, decimal unitPrice, int saleId, int productId)
        {
            var sale = await _dbContext.Sales.FindAsync(saleId) ?? throw new Exception("Sale not found");
            var product = await _dbContext.Products.FindAsync(productId) ?? throw new Exception("Product not found");

            var saleDetail = new SalesDetails
            {
                Quantity = quantity,
                UnitPrice = unitPrice,
                Sales = sale,
                Products = product,
            };

            try
            {
                await _dbContext.SalesDetails.AddAsync(saleDetail);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateSaleDetail(int idSaleDetail, int quantity, decimal unitPrice, int saleId, int productId)
        {
            var saleDetail = await _dbContext.SalesDetails.FindAsync(idSaleDetail) ?? throw new Exception("Sale Detail not found");
            var sale = await _dbContext.Sales.FindAsync(saleId) ?? throw new Exception("Sale not found");
            var product = await _dbContext.Products.FindAsync(productId) ?? throw new Exception("Product not found");

            // Update
            saleDetail.Quantity = quantity;
            saleDetail.UnitPrice = unitPrice;
            saleDetail.Sales = sale;
            saleDetail.Products = product;

            try
            {
                _dbContext.SalesDetails.Update(saleDetail);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;

            }
        }
    }
}
