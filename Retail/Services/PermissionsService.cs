using Retail.Model;
using Retail.Repositories;

namespace Retail.Services
{
    public interface IPermissionsService
    {
        Task<IEnumerable<Permissions>> GetPermissions();
        Task<Permissions> GetPermissionsById(int permissionid);
        Task CreatePermissions(string permission);
        Task UpdatePermissions(int permissionId, string permission);
        Task SoftDeletePermissions(int permissionid);
    }
    public class PermissionsService : IPermissionsService
    {
        private readonly IPermissionsRepository _permissionsRepository;
        public PermissionsService(IPermissionsRepository permissionsRepository)
        {
            _permissionsRepository = permissionsRepository;
        }
        public async Task<IEnumerable<Permissions>> GetPermissions()
        {
            return await _permissionsRepository.GetPermissions();
        }
        public async Task<Permissions> GetPermissionsById(int permissionid)
        {
            return await _permissionsRepository.GetPermissionsById(permissionid);
        }

        public async Task CreatePermissions(string permission)
        {
            await _permissionsRepository.CreatePermissions(permission);
        }
        public async Task UpdatePermissions(int permissionId, string permission)
        {
            await _permissionsRepository.UpdatePermissions(permissionId, permission);
        }
        public async Task SoftDeletePermissions(int permissionid)
        {
            await _permissionsRepository.SoftDeletePermissions(permissionid);
        }
    }
}
