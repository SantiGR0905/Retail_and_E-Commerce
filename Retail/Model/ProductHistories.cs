namespace Retail.Model
{
    public class ProductHistories
    {
        public int ProductHistoryId { get; set; }
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string Description { get; set; }
        public required string CreationDate { get; set; }
        public required string Price { get; set; }
        public required string Active { get; set; }
        public required string Image { get; set; }
        public required string Stock { get; set; }
        public required string Categories { get; set; }
        public required string Modified { get; set; }
        public required string ModifiedBy { get; set; }
    }
}
