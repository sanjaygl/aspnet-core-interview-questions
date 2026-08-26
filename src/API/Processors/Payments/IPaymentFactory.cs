namespace API.Processors.Payments
{
    public interface IPaymentFactory
    {
        IPaymentProcessor CreatePaymentProcessor();

        IRefundProcessor CreateRefundProcessor();
    }
}
