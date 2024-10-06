namespace Retail.Model
{
    public class ProductHistories
    {
        public int ProductHistoryId { get; set; }
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string Description { get; set; }
        public required DateTime CreationDate { get; set; }
        public required int Active { get; set; }
        public required string Model3D { get; set; }
        public required string Categories { get; set; }
        public required DateTime Modified { get; set; }
        public required int ModifiedBy { get; set; }
    }
}
