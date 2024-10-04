using System.ComponentModel.DataAnnotations;

namespace Retail.Model
{
    public class Permissions
    {
        [Key]
        public int PermissionId { get; set; }
        public required int Permission { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
