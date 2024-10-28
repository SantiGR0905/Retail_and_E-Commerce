using System.ComponentModel.DataAnnotations;

namespace Retail.Model.Dto
{
    public class SalesDto
    {
        [Key]
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public int StateSale { get; set; }
        public String Direction { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
