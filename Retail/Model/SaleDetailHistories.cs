namespace Retail.Model
{
    public class SaleDetailHistories
    {
        public int SaleDetailHistoryId { get; set; }
        public int SaleDetailId { get; set; }
        public required string Quantity { get; set; }
        public required string UnitPrice { get; set; }
        public required string Sales {  get; set; }
        public required string Products { get; set; }

        public required string Modified { get; set; }
        public required string ModifiedBy { get; set; }
    }
}
