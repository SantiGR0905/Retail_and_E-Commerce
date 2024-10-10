using System.ComponentModel.DataAnnotations;

namespace Retail.Model
{
    public class Permissions
    {
        public int PermissionId { get; set; }
        public required string Permission { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
