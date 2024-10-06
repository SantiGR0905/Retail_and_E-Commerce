using System.ComponentModel.DataAnnotations;

namespace Retail.Model
{
    public class Sales
    {
        public int SaleId { get; set; }
        public required DateTime SaleDate { get; set; }
        public required int StateSale { get; set; }
        public required String Direction { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual required Users Users { get; set; }
        public virtual required Products Products { get; set; }
    }
}
