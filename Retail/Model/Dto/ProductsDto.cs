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
        public int Active { get; set; }
        public string Model3D { get; set; }
        public int CategoryId { get; set; }
    }
}
