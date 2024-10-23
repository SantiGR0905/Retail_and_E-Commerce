using Microsoft.EntityFrameworkCore;
using Retail.Context;
using Retail.Model;

namespace Retail.Repositories
{
    public interface IUserTypesRepository
    {
        Task<IEnumerable<UserTypes>> GetUserTypes();
        Task<UserTypes> GetUserTypesById(int idusertype);
        Task CreateUserTypes(string userType);
        Task UpdateUserTypes(int idusertype, string userType);
        Task SoftDeleteUserTypes(int idusertype);
    }
    public class UserTypesRepository : IUserTypesRepository
    {
        private readonly RetailDbContext _dbContext;

        public UserTypesRepository(RetailDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<UserTypes>> GetUserTypes()
        {
            return await _dbContext.UserTypes
                .Where(s => !s.IsDeleted)
                .ToListAsync();
        }

        public async Task<UserTypes> GetUserTypesById(int idusertype)
        {
            return await _dbContext.UserTypes.AsNoTracking()
                .FirstOrDefaultAsync(s => s.UserTypeId == idusertype && !s.IsDeleted);
        }
        public async Task SoftDeleteUserTypes(int idusertypes)
        {
            var usertypes = await _dbContext.UserTypes.FindAsync(idusertypes);
            if (usertypes != null)
            {
                usertypes.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task CreateUserTypes(string userType)
        {
            var usertype = new UserTypes
            {
                UserType = userType
            };
            await _dbContext.UserTypes.AddAsync(usertype);    
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserTypes(int idusertype, string userType)
        {
            var usertype = await _dbContext.UserTypes.FindAsync(idusertype) ?? throw new Exception("UserType not found");

            usertype.UserType = userType;

            try
            {
                _dbContext.UserTypes.Update(usertype);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception e)
            {

                throw;

            }
        }
    }
}
