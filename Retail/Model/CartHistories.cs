using Newtonsoft.Json;

namespace Retail.Model
{
    public class CartHistories
    {
        public int CartHistoryId { get; set; }
        public int CartId { get; set; }
        public required string Created {  get; set; }
        public required string IsActive { get; set; }
        public required string Users {  get; set; }
        public required string Modified { get; set; }
        public required string ModifiedBy { get; set; }
    }
}
