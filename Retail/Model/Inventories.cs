using System.ComponentModel.DataAnnotations;

namespace Retail.Model
{
    public class Inventories
    {
        public int InventoryId { get; set; }
        public required int Amount { get; set; }
        public required DateTime LastUpdate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual required Products Products { get; set; }
    }
}
