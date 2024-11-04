using System.ComponentModel.DataAnnotations;

namespace Retail.Model.Dto
{
    public class InventoriesDto
    {
        [Key]
        public int InventoryId { get; set; }
        public int Amount { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
