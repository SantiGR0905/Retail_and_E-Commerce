using System.ComponentModel.DataAnnotations;

namespace Retail.Model.Dto
{
    public class CartItemsDto
    {
        [Key]
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
    }
}
