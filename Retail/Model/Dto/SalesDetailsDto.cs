using System.ComponentModel.DataAnnotations;

namespace Retail.Model.Dto
{
    public class SalesDetailsDto
    {
        [Key]
        public int SaleDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
    }
}
