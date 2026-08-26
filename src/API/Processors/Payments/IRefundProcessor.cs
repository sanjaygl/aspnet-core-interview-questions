using API.Services.Payment.Models;

namespace API.Processors.Payments
{
    public interface IRefundProcessor
    {
        Task<PaymentResponse> RefundAsync(PaymentRequest request, CancellationToken cancellationToken);
    }
}
