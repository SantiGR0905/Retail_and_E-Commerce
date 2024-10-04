using Retail.Model;
using Retail.Repositories;

namespace Retail.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<Users>> GetUsers();
        Task<Users> GetUsersById(int idUser);
        Task CreateUsers(Users user);
        Task UpdateUsers(Users user);
        Task SoftDeleteUsers(int idUser);
    }
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public async Task<IEnumerable<Users>> GetUsers()
        {
            return await _usersRepository.GetUsers();
        }
        public async Task<Users> GetUsersById(int idUser)
        {
            return await _usersRepository.GetUsersById(idUser);
        }

        public async Task CreateUsers(Users user)
        {
            await _usersRepository.CreateUsers(user);
        }
        public async Task UpdateUsers(Users user)
        {
            await _usersRepository.UpdateUsers(user);
        }
        public async Task SoftDeleteUsers(int idUser)
        {
            await _usersRepository.SoftDeleteUsers(idUser);
        }
    }
}
