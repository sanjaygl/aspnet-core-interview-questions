using API.Processors.Payments;
using API.Services.Payment.Models;

namespace API.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentFactoryResolver _paymentProcessorResolver;

        public PaymentService(PaymentFactoryResolver paymentProcessorFactory)
        {
            _paymentProcessorResolver = paymentProcessorFactory;
        }

        public async Task<Models.PaymentResponse> ProcessPaymentAsync(PaymentRequest request, CancellationToken cancellationToken = default)
        {
            var factory = _paymentProcessorResolver.GetFactory(request.PaymentMethod);

            var processor = factory.CreatePaymentProcessor();

            return await processor.ProcessAsync(request, cancellationToken);
        }

        public async Task<PaymentResponse> RefundPaymentAsync(PaymentRequest request, CancellationToken cancellationToken = default)
        {
            var factory = _paymentProcessorResolver.GetFactory(request.PaymentMethod);

            var processor = factory.CreateRefundProcessor();

            return await processor.RefundAsync(request, cancellationToken);
        }
    }
}
