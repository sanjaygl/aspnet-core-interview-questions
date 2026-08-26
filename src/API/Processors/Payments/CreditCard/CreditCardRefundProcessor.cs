using API.Services.Payment.Models;

namespace API.Processors.Payments
{
    public class CreditCardRefundProcessor : IRefundProcessor
    {
        public async Task<PaymentResponse> RefundAsync(
            PaymentRequest request,
            CancellationToken cancellationToken)
        {
            return new PaymentResponse
            {
                Success = true,
                Message = "Refund processed using Credit Card."
            };
        }
    }
}