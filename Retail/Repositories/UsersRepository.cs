using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Retail.Context;
using Retail.Model;

namespace Retail.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<Users>> GetUsers();
        Task<Users> GetUsersById(int idUser);
        Task CreateUsers(Users user);
        Task UpdateUsers(Users user);
        Task SoftDeleteUsers(int idUser);
        Task<bool> ValidateUserAsync(string email, string password);
    }
    public class UsersRepository : IUsersRepository
    {
        private readonly RetailDbContext _dbContext;

        public UsersRepository(RetailDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Users>> GetUsers()
        {
            return await _dbContext.Users
                .Where(s => !s.IsDeleted)
                .ToListAsync();
        }

        public async Task<Users> GetUsersById(int idUser)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(s => s.UserId == idUser && !s.IsDeleted);
        }
        public async Task SoftDeleteUsers(int idUser)
        {
            var users = await _dbContext.Users.FindAsync(idUser);
            if (users != null)
            {
                users.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task CreateUsers(Users user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUsers(Users user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> ValidateUserAsync(string email, string password)
        {
            // Fetch the user by email
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception("User not found");

            // User does not exist
            if (user == null) return false;

            // Initialize PasswordHasher
            var passwordHasher = new PasswordHasher<Users>();

            // Verify the password
            var userVerification = passwordHasher.VerifyHashedPassword(user, user.Password, password);

            // Check if password is correct
            if (userVerification == PasswordVerificationResult.Success) return true;

            // Password is invalid
            return false;
        }
    }
}
