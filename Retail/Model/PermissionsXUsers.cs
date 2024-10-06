using System.ComponentModel.DataAnnotations;

namespace Retail.Model
{
    public class PermissionsXUsers
    {
        public int PermissionXUserId { get; set; }
        public bool IsDeleted { get; internal set; } = false;
        public virtual required UserTypes UserTypes { get; set; }
        public virtual required Permissions Permissions { get; set; }
    }
}
