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
        Task CreateUsers(string firstName, string lastName, string email, string password, int userTypeId);
        Task UpdateUsers(int idUser ,string firstName, string lastName, string email, string password, DateTime originalDate, int userTypeId);
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
                .Include(u => u.UserTypes)
                .ToListAsync();
        }

        public async Task<Users> GetUsersById(int idUser)
        {
            return await _dbContext.Users.AsNoTracking()
                .Include(u => u.UserTypes)
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

        public async Task CreateUsers(string firstName, string lastName, string email, string password, int userTypeId)
        {
            var userType = await _dbContext.UserTypes.FindAsync(userTypeId) ?? throw new Exception("UserType not found");

            // Hash the password
            var passwordHasher = new PasswordHasher<Users>();
            var hashedPassword = passwordHasher.HashPassword(null, password);

            // Create a new User object
            var user = new Users
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = hashedPassword,
                Date = DateTime.Now,
                UserTypes = userType
            };

            try
            {
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateUsers(int idUser, string firstName, string lastName, string email, string password, DateTime originalDate, int userTypeId)
        {
            // Find the existing user by ID
            var user = await _dbContext.Users.FindAsync(idUser) ?? throw new Exception("User not found");

            // Fetch the User object based on userId and attendantId
            var userType = await _dbContext.UserTypes.FindAsync(userTypeId) ?? throw new Exception("UserType not found");

            // Hash the password
            var passwordHasher = new PasswordHasher<Users>();
            var hashedPassword = passwordHasher.HashPassword(user, password);

            // Update
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;
            user.Password = hashedPassword;
            user.Date = originalDate;
            user.UserTypes = userType;

            try
            {
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;

            }
        }
        public async Task<bool> ValidateUserAsync(string email, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception("User not found");

            if (user == null) return false;

            var passwordHasher = new PasswordHasher<Users>();

            var userVerification = passwordHasher.VerifyHashedPassword(user, user.Password, password);

            if (userVerification == PasswordVerificationResult.Success) return true;

            return false;
        }
    }
}
