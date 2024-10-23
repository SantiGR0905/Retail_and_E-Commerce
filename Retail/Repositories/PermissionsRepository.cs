using Microsoft.EntityFrameworkCore;
using Retail.Context;
using Retail.Model;

namespace Retail.Repositories
{
    public interface IPermissionsRepository
    {
        Task<IEnumerable<Permissions>> GetPermissions();
        Task<Permissions> GetPermissionsById(int permissionid);
        Task CreatePermissions(string permission);
        Task UpdatePermissions(int permissionId, string permission);
        Task SoftDeletePermissions(int permissionid);
    }
    public class PermissionsRepository : IPermissionsRepository
    {
        private readonly RetailDbContext _dbContext;

        public PermissionsRepository(RetailDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Permissions>> GetPermissions()
        {
            return await _dbContext.Permissions
                .Where(s => !s.IsDeleted)
                .ToListAsync();
        }

        public async Task<Permissions> GetPermissionsById(int permissionid)
        {
            return await _dbContext.Permissions
                .FirstOrDefaultAsync(s => s.PermissionId == permissionid && !s.IsDeleted);
        }
        public async Task SoftDeletePermissions(int permissionid)
        {
            var permissions = await _dbContext.Permissions.FindAsync(permissionid);
            if (permissions != null)
            {
                permissions.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task CreatePermissions(string permission)
        {
            var permissions = new Permissions
            {
                Permission = permission
            };
            await _dbContext.Permissions.AddAsync(permissions);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePermissions(int permissionId, string permission)
        {
            var permissions = await _dbContext.Permissions.FindAsync(permissionId) ?? throw new Exception("Permission not found");

            permissions.Permission = permission;

            try
            {
                _dbContext.Permissions.Update(permissions);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception e)
            {

                throw;

            }
        }
    }
}
