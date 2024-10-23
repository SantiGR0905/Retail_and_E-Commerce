using Microsoft.EntityFrameworkCore;
using Retail.Context;
using Retail.Model;

namespace Retail.Repositories
{
    public interface IPermissionsXUsersRepository
    {
        Task<IEnumerable<PermissionsXUsers>> GetPermissionsXUsers();
        Task<PermissionsXUsers> GetPermissionsXUsersById(int idpermissionxuser);
        Task CreatePermissionsXUsers(int userTypeId, int permissionId);
        Task UpdatePermissionsXUsers(int idpermissionxuser ,int userTypeId, int permissionId);
        Task SoftDeletePermissionsXUsers(int idpermissionxuser);
        Task<bool> HasPermissionAsync(int userTypeId, int permissionId);
    }
    public class PermissionsXUsersRepository : IPermissionsXUsersRepository
    {
        private readonly RetailDbContext _dbContext;

        public PermissionsXUsersRepository(RetailDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<PermissionsXUsers>> GetPermissionsXUsers()
        {
            return await _dbContext.PermissionsXUsers
                .Where(s => !s.IsDeleted)
                .Include(p => p.Permissions)
                .Include(u => u.UserTypes)
                .ToListAsync();
        }

        public async Task<PermissionsXUsers> GetPermissionsXUsersById(int idpermissionxuser)
        {
            return await _dbContext.PermissionsXUsers
                .Include(p => p.Permissions)
                .Include(u => u.UserTypes)
                .FirstOrDefaultAsync(s => s.PermissionXUserId == idpermissionxuser && !s.IsDeleted);
        }
        public async Task SoftDeletePermissionsXUsers(int idpermissionxuser)
        {
            var permissionxuser = await _dbContext.PermissionsXUsers.FindAsync(idpermissionxuser);
            if (permissionxuser != null)
            {
                permissionxuser.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task CreatePermissionsXUsers(int userTypeId, int permissionId)
        {
            var userType = await _dbContext.UserTypes.FindAsync(userTypeId) ?? throw new Exception("UserType not found");
            var permission = await _dbContext.Permissions.FindAsync(permissionId) ?? throw new Exception("Permission not found");

            var permissionxuser = new PermissionsXUsers
            {
                UserTypes = userType,
                Permissions = permission
            };

            try
            {
                await _dbContext.PermissionsXUsers.AddAsync(permissionxuser);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdatePermissionsXUsers(int idpermissionxuser, int userTypeId, int permissionId)
        {
            var permissionxuser = await _dbContext.PermissionsXUsers.FindAsync(idpermissionxuser) ?? throw new Exception("PermissionXUser not found");

            var userType = await _dbContext.UserTypes.FindAsync(userTypeId) ?? throw new Exception("UserType not found");
            var permission = await _dbContext.Permissions.FindAsync(permissionId) ?? throw new Exception("Permission not found");

            // Update
            permissionxuser.UserTypes = userType;
            permissionxuser.Permissions = permission;

            try
            {
                _dbContext.PermissionsXUsers.Update(permissionxuser);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;

            }
        }
        public async Task<bool> HasPermissionAsync(int userTypeId, int permissionId)
        {
            var permission = await _dbContext.PermissionsXUsers
            .Where(p => p.UserTypes.UserTypeId == userTypeId && p.Permissions.PermissionId == permissionId && !p.IsDeleted)
            .FirstOrDefaultAsync();

            return permission != null ? true : false;
        }
    }
}
