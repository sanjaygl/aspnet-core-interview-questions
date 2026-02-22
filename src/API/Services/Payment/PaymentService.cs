namespace API.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        public async Task<Models.PaymentResponse> ProcessPaymentAsync(Models.PaymentRequest request, CancellationToken cancellationToken = default)
        {
            // Simulate processing delay
            await Task.Delay(50, cancellationToken);

            // Dummy implementation - always succeeds for demo
            return new Models.PaymentResponse
            {
                Success = true,
                TransactionId = Guid.NewGuid().ToString("N"),
                Message = "Processed"
            };
        }
    }
}
