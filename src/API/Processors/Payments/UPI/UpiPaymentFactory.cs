using API.Processors.Payments.UPI;

namespace API.Processors.Payments
{
    public class UpiPaymentFactory : IPaymentFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public UpiPaymentFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPaymentProcessor CreatePaymentProcessor()
        {
            return _serviceProvider
                .GetRequiredService<UpiPaymentProcessor>();
        }

        public IRefundProcessor CreateRefundProcessor()
        {
            return _serviceProvider
                .GetRequiredService<UpiRefundProcessor>();
        }
    }
}