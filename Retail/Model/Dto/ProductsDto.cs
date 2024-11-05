using System.ComponentModel.DataAnnotations;

namespace Retail.Model.Dto
{
    public class ProductsDto
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Stock {get; set;}
        public int CategoryId { get; set; }
    }
}
