using System.ComponentModel.DataAnnotations;

namespace Retail.Model
{
    public class Products
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string Description { get; set; }
        public required DateTime CreationDate { get; set; }
        public required decimal Price { get; set; }
        public required bool Active { get; set; }
        public required string Image { get; set; }
        public required int Stock {  get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual required Categories Categories { get; set; }
    }
}
