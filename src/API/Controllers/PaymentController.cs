using Microsoft.AspNetCore.Mvc;
using API.Services.Payment;
using API.Services.Payment.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<ActionResult<PaymentResponse>> Post([FromBody] PaymentRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest("Request body is required.");

            if (request.Amount <= 0)
                return BadRequest("Amount must be greater than zero.");

            var result = await _paymentService.ProcessPaymentAsync(request, cancellationToken);

            if (result.Success)
                return CreatedAtAction(null, new { id = result.TransactionId }, result);

            return BadRequest(result.Message);
        }
    }
}
