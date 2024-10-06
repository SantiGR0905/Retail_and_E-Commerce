using System.ComponentModel.DataAnnotations;

namespace Retail.Model
{
    public class Users
    {
        public int UserId { get; set; }

        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required DateTime Date { get; set; }
        public required DateTime Modified { get; set; }
        public required int ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual required UserTypes UserTypes { get; set; }
    }
}
