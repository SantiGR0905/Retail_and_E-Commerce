using System.ComponentModel.DataAnnotations;

namespace Retail.Model.Dto
{
    public class CartsDto
    {
        [Key]
        public int CartId { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
    }
}
