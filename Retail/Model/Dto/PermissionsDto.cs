using System.ComponentModel.DataAnnotations;

namespace Retail.Model.Dto
{
    public class PermissionsDto
    {
        [Key]
        public int PermissionId { get; set; }
        public string Permission { get; set; }
    }
}
