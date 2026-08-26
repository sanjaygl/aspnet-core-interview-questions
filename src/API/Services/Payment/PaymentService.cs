using API.Processors.Payments;
using API.Services.Payment.Models;

namespace API.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentProcessorFactory _paymentProcessorFactory;

        public PaymentService(PaymentProcessorFactory paymentProcessorFactory)
        {
            _paymentProcessorFactory = paymentProcessorFactory;
        }

        public async Task<Models.PaymentResponse> ProcessPaymentAsync(PaymentRequest request, CancellationToken cancellationToken = default)
        {
            var processor = _paymentProcessorFactory.Create(request.PaymentMethod);

            return await processor.ProcessAsync(request, cancellationToken);
        }
    }
}
