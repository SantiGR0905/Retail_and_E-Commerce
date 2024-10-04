using System.ComponentModel.DataAnnotations;

namespace Retail.Model
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        public required String CategoryName { get; set; }
        public required String CategoryDescription { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
