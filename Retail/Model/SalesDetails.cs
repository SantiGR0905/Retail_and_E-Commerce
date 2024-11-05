namespace Retail.Model
{
    public class SalesDetails
    {
        public int SaleDetailId { get; set; }
        public required int Quantity { get; set; }
        public required decimal UnitPrice { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual required Sales Sales { get; set; }
        public virtual required Products Products { get; set; }
    }
}
