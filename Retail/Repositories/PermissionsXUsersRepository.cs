using Microsoft.EntityFrameworkCore;
using Retail.Context;
using Retail.Model;

namespace Retail.Repositories
{
    public interface IPermissionsXUsersRepository
    {
        Task<IEnumerable<PermissionsXUsers>> GetPermissionsXUsers();
        Task<PermissionsXUsers> GetPermissionsXUsersById(int idpermissionxuser);
        Task CreatePermissionsXUsers(PermissionsXUsers permissionxuser);
        Task UpdatePermissionsXUsers(PermissionsXUsers permissionxuser);
        Task SoftDeletePermissionsXUsers(int idpermissionxuser);
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
                .ToListAsync();
        }

        public async Task<PermissionsXUsers> GetPermissionsXUsersById(int idpermissionxuser)
        {
            return await _dbContext.PermissionsXUsers
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

        public async Task CreatePermissionsXUsers(PermissionsXUsers permissionxuser)
        {
            _dbContext.PermissionsXUsers.Add(permissionxuser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePermissionsXUsers(PermissionsXUsers permissionxuser)
        {
            _dbContext.PermissionsXUsers.Update(permissionxuser);
            await _dbContext.SaveChangesAsync();
        }
    }
}
