using System.ComponentModel.DataAnnotations;

namespace Retail.Model.Dto
{
    public class CategoriesDto
    {
        [Key]
        public int CategoryId { get; set; }
        public String CategoryName { get; set; }
        public String CategoryDescription { get; set; }
    }
}
