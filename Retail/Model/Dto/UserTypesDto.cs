using System.ComponentModel.DataAnnotations;

namespace Retail.Model.Dto
{
    public class UserTypesDto
    {
        [Key]
        public int UserTypeId { get; set; }
        public string UserType { get; set; }
    }
}
