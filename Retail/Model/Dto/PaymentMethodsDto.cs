using System.ComponentModel.DataAnnotations;

namespace Retail.Model.Dto
{
    public class PaymentMethodsDto
    {
        [Key]
        public int PaymentMethodId { get; set; }
        public string MethodName { get; set; }
        public string DescriptionMethod { get; set; }
    }
}
