using Retail.Model;
using Retail.Repositories;

namespace Retail.Services
{
    public interface IPermissionsXUsersService
    {
        Task<IEnumerable<PermissionsXUsers>> GetPermissionsXUsers();
        Task<PermissionsXUsers> GetPermissionsXUsersById(int idpermissionxuser);
        Task CreatePermissionsXUsers(PermissionsXUsers permissionxuser);
        Task UpdatePermissionsXUsers(PermissionsXUsers permissionxuser);
        Task SoftDeletePermissionsXUsers(int idpermissionxuser);
    }
    public class PermissionsXUsersService : IPermissionsXUsersService
    {
        private readonly IPermissionsXUsersRepository _permissionsXUsersRepository;
        public PermissionsXUsersService(IPermissionsXUsersRepository permissionsXUsersRepository)
        {
            _permissionsXUsersRepository = permissionsXUsersRepository;
        }
        public async Task<IEnumerable<PermissionsXUsers>> GetPermissionsXUsers()
        {
            return await _permissionsXUsersRepository.GetPermissionsXUsers();
        }
        public async Task<PermissionsXUsers> GetPermissionsXUsersById(int idpermissionxuser)
        {
            return await _permissionsXUsersRepository.GetPermissionsXUsersById(idpermissionxuser);
        }

        public async Task CreatePermissionsXUsers(PermissionsXUsers permissionxuser)
        {
            await _permissionsXUsersRepository.CreatePermissionsXUsers(permissionxuser);
        }
        public async Task UpdatePermissionsXUsers(PermissionsXUsers permission)
        {
            await _permissionsXUsersRepository.UpdatePermissionsXUsers(permission);
        }
        public async Task SoftDeletePermissionsXUsers(int idpermissionxuser)
        {
            await _permissionsXUsersRepository.SoftDeletePermissionsXUsers(idpermissionxuser);
        }
    }
}
