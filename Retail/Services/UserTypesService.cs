using Retail.Model;
using Retail.Repositories;

namespace Retail.Services
{
    public interface IUserTypesService
    {
        Task<IEnumerable<UserTypes>> GetUserTypes();
        Task<UserTypes> GetUserTypesById(int idusertype);
        Task CreateUserTypes(string userType);
        Task UpdateUserTypes(int idusertype, string userType);
        Task SoftDeleteUserTypes(int idusertype);
    }
    public class UserTypesService : IUserTypesService
    {
        private readonly IUserTypesRepository _userTypesRepository;
        public UserTypesService(IUserTypesRepository userTypesRepository)
        {
            _userTypesRepository = userTypesRepository;
        }
        public async Task<IEnumerable<UserTypes>> GetUserTypes()
        {
            return await _userTypesRepository.GetUserTypes();
        }
        public async Task<UserTypes> GetUserTypesById(int idusertype)
        {
            return await _userTypesRepository.GetUserTypesById(idusertype);
        }

        public async Task CreateUserTypes(string userType)
        {
            await _userTypesRepository.CreateUserTypes(userType);
        }
        public async Task UpdateUserTypes(int idusertype, string userType)
        {
            await _userTypesRepository.UpdateUserTypes(idusertype, userType);
        }
        public async Task SoftDeleteUserTypes(int idusertype)
        {
            await _userTypesRepository.SoftDeleteUserTypes(idusertype);
        }
    }
}
