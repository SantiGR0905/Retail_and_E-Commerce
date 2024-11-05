using System.Globalization;

namespace Retail.Model
{
    public class PaymentMethodHistories
    {
        public int PaymentMethodHistoryId { get; set; }
        public int PaymentMethodsId { get; set; }
        public required string MethodName { get; set; }
        public required string DescriptionMethod { get; set; }

        public required string Modified { get; set; }
        public required string ModifiedBy { get; set; }

    }
}
