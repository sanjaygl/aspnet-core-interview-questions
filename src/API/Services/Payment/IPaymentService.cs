using API.Services.Payment.Models;

namespace API.Services.Payment
{
    public interface IPaymentService
    {
        Task<PaymentResponse> ProcessPaymentAsync(PaymentRequest request, CancellationToken cancellationToken = default);
    }
}