using Retail.Model;
using Retail.Repositories;

namespace Retail.Services
{
    public interface IUserTypesService
    {
        Task<IEnumerable<UserTypes>> GetUserTypes();
        Task<UserTypes> GetUserTypesById(int idusertype);
        Task CreateUserTypes(UserTypes usertypes);
        Task UpdateUserTypes(UserTypes usertypes);
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

        public async Task CreateUserTypes(UserTypes usertypes)
        {
            await _userTypesRepository.CreateUserTypes(usertypes);
        }
        public async Task UpdateUserTypes(UserTypes usertypes)
        {
            await _userTypesRepository.UpdateUserTypes(usertypes);
        }
        public async Task SoftDeleteUserTypes(int idusertype)
        {
            await _userTypesRepository.SoftDeleteUserTypes(idusertype);
        }
    }
}
