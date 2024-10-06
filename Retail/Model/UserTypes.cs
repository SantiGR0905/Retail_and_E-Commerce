using System.ComponentModel.DataAnnotations;

namespace Retail.Model
{
    public class UserTypes
    {
        public int UserTypeId { get; set; }
        public required string UserType { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
