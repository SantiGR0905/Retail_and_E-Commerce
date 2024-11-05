using Microsoft.EntityFrameworkCore;
using Retail.Context;
using Retail.Model;

namespace Retail.Repositories
{
    public interface IPaymentMethodsRepository
    {
        Task<IEnumerable<PaymentMethods>> GetPaymentMethod();
        Task<PaymentMethods> GetPaymentMethodById(int idPaymentMethod);
        Task CreatePaymentMethod(string methodName, string descriptionMethod);
        Task UpdatePaymentMethod(int idPaymentMethod, string methodName, string descriptionMethod);
        Task SoftDeletePaymentMethod(int idPaymentMethod);
    }
    public class PaymentMethodsRepository : IPaymentMethodsRepository
    {
        private readonly RetailDbContext _dbContext;

        public PaymentMethodsRepository(RetailDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<PaymentMethods>> GetPaymentMethod()
        {
            return await _dbContext.PaymentMethods
                .Where(s => !s.IsDeleted)
                .ToListAsync();
        }

        public async Task<PaymentMethods> GetPaymentMethodById(int idPaymentMethod)
        {
            return await _dbContext.PaymentMethods
                .FirstOrDefaultAsync(s => s.PaymentMethodId == idPaymentMethod && !s.IsDeleted);
        }
        public async Task SoftDeletePaymentMethod(int idPaymentMethod)
        {
            var PaymentMethod = await _dbContext.PaymentMethods.FindAsync(idPaymentMethod);
            if (PaymentMethod != null)
            {
                PaymentMethod.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task CreatePaymentMethod(string methodName, string descriptionMethod)
        {
            var paymentMethod = new PaymentMethods
            {
                MethodName = methodName,
                DescriptionMethod = descriptionMethod
            };
            await _dbContext.PaymentMethods.AddAsync(paymentMethod);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePaymentMethod(int idPaymentMethod, string methodName, string descriptionMethod)
        {
            var paymentMethod = await _dbContext.PaymentMethods.FindAsync(idPaymentMethod) ?? throw new Exception("PaymentMethod not found");

            paymentMethod.MethodName = methodName;
            paymentMethod.DescriptionMethod = descriptionMethod;

            try
            {
                _dbContext.PaymentMethods.Update(paymentMethod);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception e)
            {

                throw;

            }
        }
    }
}
