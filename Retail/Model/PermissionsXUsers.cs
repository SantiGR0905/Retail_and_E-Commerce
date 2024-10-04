using System.ComponentModel.DataAnnotations;

namespace Retail.Model
{
    public class PermissionsXUsers
    {
        [Key]
        public int PermissionXUserId { get; set; }
        public int PermissionId { get; set; }
        public int UserTypeId { get; set; }
        public bool IsDeleted { get; internal set; } = false;
    }
}
