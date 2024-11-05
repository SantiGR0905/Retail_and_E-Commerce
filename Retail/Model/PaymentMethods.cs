using System.ComponentModel.DataAnnotations;

namespace Retail.Model
{
    public class PaymentMethods
    {
        public int PaymentMethodId { get; set; }
        public required string MethodName { get; set; }
        public required string DescriptionMethod { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
