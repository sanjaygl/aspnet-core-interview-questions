using API.Processors.Payments.CreditCard;

namespace API.Processors.Payments
{
    public class CreditCardPaymentFactory : IPaymentFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CreditCardPaymentFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPaymentProcessor CreatePaymentProcessor()
        {
            return _serviceProvider
                .GetRequiredService<CreditCardPaymentProcessor>();
        }

        public IRefundProcessor CreateRefundProcessor()
        {
            return _serviceProvider
                .GetRequiredService<CreditCardRefundProcessor>();
        }
    }
}