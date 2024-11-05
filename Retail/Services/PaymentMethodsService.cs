using Retail.Model;
using Retail.Repositories;

namespace Retail.Services
{
    public interface IPaymentMethodsService
    {
        Task<IEnumerable<PaymentMethods>> GetPaymentMethod();
        Task<PaymentMethods> GetPaymentMethodById(int idPaymentMethod);
        Task CreatePaymentMethod(string methodName, string descriptionMethod);
        Task UpdatePaymentMethod(int idPaymentMethod, string methodName, string descriptionMethod);
        Task SoftDeletePaymentMethod(int idPaymentMethod);
    }
    public class PaymentMethodsService : IPaymentMethodsService
    {
        private readonly IPaymentMethodsRepository _PaymentMethodsRepository;
        public PaymentMethodsService(IPaymentMethodsRepository PaymentMethodsRepository)
        {
            _PaymentMethodsRepository = PaymentMethodsRepository;
        }
        public async Task<IEnumerable<PaymentMethods>> GetPaymentMethod()
        {
            return await _PaymentMethodsRepository.GetPaymentMethod();
        }
        public async Task<PaymentMethods> GetPaymentMethodById(int idPaymentMethod)
        {
            return await _PaymentMethodsRepository.GetPaymentMethodById(idPaymentMethod);
        }

        public async Task CreatePaymentMethod(string methodName, string descriptionMethod)
        {
            await _PaymentMethodsRepository.CreatePaymentMethod(methodName, descriptionMethod);
        }
        public async Task UpdatePaymentMethod(int idPaymentMethod, string methodName, string descriptionMethod)
        {
            await _PaymentMethodsRepository.UpdatePaymentMethod(idPaymentMethod, methodName, descriptionMethod);
        }
        public async Task SoftDeletePaymentMethod(int idPaymentMethod)
        {
            await _PaymentMethodsRepository.SoftDeletePaymentMethod(idPaymentMethod);
        }
    }
}
