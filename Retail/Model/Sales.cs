using System.ComponentModel.DataAnnotations;

namespace Retail.Model
{
    public class Sales
    {
        [Key]
        public int SaleId { get; set; }
        public required int UserId { get; set; }
        public required DateTime SaleDate { get; set; }
        public required int StateSale { get; set; }
        public required int ProductId { get; set; }
        public required String Direction { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
