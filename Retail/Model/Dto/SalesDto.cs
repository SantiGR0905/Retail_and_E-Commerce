using System.ComponentModel.DataAnnotations;

namespace Retail.Model.Dto
{
    public class SalesDto
    {
        [Key]
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public string StateSale { get; set; }
        public string Direction { get; set; }
        public int UserId { get; set; }
        public int PaymentMethodId { get; set; }
    }
}
