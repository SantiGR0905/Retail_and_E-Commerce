using System.ComponentModel.DataAnnotations;

namespace Retail.Model.Dto
{
    public class UserDto
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Date { get; set; }
        public int UserTypeId { get; set; }
    }
}
