namespace Retail.Model
{
    public class SaleHistories
    {
        public int SaleHistoryId { get; set; }
        public int SaleId { get; set; }
        public required DateTime SaleDate { get; set; }
        public required int StateSale { get; set; }
        public required string Direction { get; set; }
        public required string Users { get; set; }
        public required string Products { get; set; }
        public required DateTime Modified { get; set; }
        public required int ModifiedBy { get; set; }
    }
}
