namespace Retail.Model
{
    public class CartItems
    {
        public int CartItemId { get; set; }
        public required int Quantity { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual required Carts Carts { get; set; }
        public virtual required Products Products { get; set; }
    }
}
