using API.Services.Payment.Models;

namespace API.Processors.Payments
{
    public interface IPaymentProcessor
    {
        Task<PaymentResponse> ProcessAsync(PaymentRequest request, CancellationToken cancellationToken);
    }
}
