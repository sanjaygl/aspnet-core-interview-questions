using API.Services.Payment.Models;

namespace API.Processors.Payments
{
    public class UpiPaymentProcessor : IPaymentProcessor
    {
        public async Task<PaymentResponse> ProcessAsync(PaymentRequest request, CancellationToken cancellationToken)
        {
            return new PaymentResponse { Success = true, Message = "Payment processed using UPI." };
        }
    }
}
