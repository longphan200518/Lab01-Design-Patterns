using Microsoft.AspNetCore.Mvc;
using Lab01.Patterns.FactoryMethod;
using Lab01.Services;

namespace Lab01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILoggerService _logger;

        public PaymentController(ILoggerService logger)
        {
            _logger = logger;
        }

        [HttpPost("process")]
        public IActionResult ProcessPayment([FromBody] PaymentRequest request)
        {
            try
            {
                _logger.Log($"Processing payment: Type={request.PaymentType}, Amount={request.Amount}");

                // Use Factory Method to create payment method
                IPaymentMethod paymentMethod = PaymentFactory.CreatePaymentMethod(request.PaymentType);
                
                string result = paymentMethod.ProcessPayment(request.Amount, request.Description);
                
                _logger.Log($"Payment successful: {result}");

                return Ok(new
                {
                    success = true,
                    paymentType = paymentMethod.GetPaymentType(),
                    message = result,
                    timestamp = DateTime.Now
                });
            }
            catch (ArgumentException ex)
            {
                _logger.Log($"Payment failed: {ex.Message}");
                return BadRequest(new { success = false, error = ex.Message });
            }
        }

        [HttpGet("supported-types")]
        public IActionResult GetSupportedPaymentTypes()
        {
            var types = PaymentFactory.GetSupportedPaymentTypes();
            return Ok(new { supportedPaymentTypes = types });
        }
    }

    public class PaymentRequest
    {
        public string PaymentType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
