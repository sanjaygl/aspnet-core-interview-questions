namespace API.Processors.Payments
{
    public class PaymentFactoryResolver
    {
        private readonly IServiceProvider _serviceProvider;
        public PaymentFactoryResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPaymentFactory GetFactory(string paymentMethod)
        {
            return paymentMethod.ToLower() switch
            {
                "upi" => _serviceProvider.GetServices<IPaymentFactory>().OfType<UpiPaymentFactory>().First(),
                "creditcard" => _serviceProvider.GetServices<IPaymentFactory>().OfType<CreditCardPaymentFactory>().First(),
                _ => throw new ArgumentException($"Unsupported payment method: {paymentMethod}")
            };
        }
    }
}
