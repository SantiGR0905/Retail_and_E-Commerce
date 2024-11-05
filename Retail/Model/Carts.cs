using System.ComponentModel.DataAnnotations;

namespace Retail.Model
{
    public class Carts
    {
        public int CartId { get; set; }
        public required DateTime Created { get; set; }
        public required bool IsActive { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual required Users Users { get; set; }
    }
}
