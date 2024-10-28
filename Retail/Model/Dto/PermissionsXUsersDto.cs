using System.ComponentModel.DataAnnotations;

namespace Retail.Model.Dto
{
    public class PermissionsXUsersDto
    {
        [Key]
        public int PermissionXUserId { get; set; }
        public int UserTypeId { get; set; }
        public int PermissionId { get; set; }
    }
}
