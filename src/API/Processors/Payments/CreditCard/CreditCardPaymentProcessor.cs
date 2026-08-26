using API.Services.Payment.Models;

namespace API.Processors.Payments.CreditCard
{
    public class CreditCardPaymentProcessor : IPaymentProcessor
    {
        public async Task<PaymentResponse> ProcessAsync(PaymentRequest request, CancellationToken cancellationToken)
        {
            return new PaymentResponse
            {
                Success = true,
                Message = "Payment processed using Credit Card."
            };
        }
    }
}
