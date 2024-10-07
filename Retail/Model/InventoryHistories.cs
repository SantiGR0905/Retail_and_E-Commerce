namespace Retail.Model
{
    public class InventoryHistories
    {
        public int InventoryHistoryId { get; set; }
        public int InventoryId { get; set; }
        public required int Amount { get; set; }
        public required string LastUpdate { get; set; }
        public required string Products { get; set; }
        public required string Modified { get; set; }
        public required string ModifiedBy { get; set; }
    }
}
