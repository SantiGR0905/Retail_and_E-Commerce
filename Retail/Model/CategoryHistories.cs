namespace Retail.Model
{
    public class CategoryHistories
    {
        public int CategoryHistoryId { get; set; }
        public int CategoryId { get; set; } 
        public required string CategoryName { get; set; }
        public required string CategoryDescription { get; set; }

        public required string Modified { get; set; }
        public required string ModifiedBy { get; set; }
    }
}
