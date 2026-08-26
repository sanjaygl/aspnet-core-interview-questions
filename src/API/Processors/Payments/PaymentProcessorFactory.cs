namespace API.Processors.Payments
{
    public class PaymentProcessorFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public PaymentProcessorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPaymentProcessor Create(string paymentMethod)
        {
            return paymentMethod.ToLower() switch
            {
                "upi" => _serviceProvider.GetRequiredService<UpiPaymentProcessor>(),
                "creditcard" => _serviceProvider.GetRequiredService<CreditCardPaymentProcessor>(),
                _ => throw new ArgumentException($"Unsupported payment method: {paymentMethod}")
            };
        }
    }
}
