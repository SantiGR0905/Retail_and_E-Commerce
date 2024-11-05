namespace Retail.Model
{
    public class CartItemHistories
    {
        public int CartItemHistoryId { get; set; }
        public int CartItemId { get; set; }
        public required string Quantity { get; set; }
        public required string Carts { get; set; }
        public required string Products { get; set; }
        public required string Modified { get; set; }
        public required string ModifiedBy { get; set; }
    }
}
