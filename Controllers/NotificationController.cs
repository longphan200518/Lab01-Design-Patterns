using Microsoft.AspNetCore.Mvc;
using Lab01.Patterns.AbstractFactory;
using Lab01.Services;

namespace Lab01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly ILoggerService _logger;

        public NotificationController(ILoggerService logger)
        {
            _logger = logger;
        }

        [HttpPost("send-email")]
        public IActionResult SendEmail([FromBody] EmailRequest request)
        {
            _logger.Log($"Sending email via {request.NotificationTier} tier");

            // Select appropriate factory based on tier
            INotificationFactory factory = GetFactory(request.NotificationTier);
            
            // Create email notification using the factory
            IEmailNotification emailNotification = factory.CreateEmailNotification();
            string result = emailNotification.SendEmail(request.Recipient, request.Subject, request.Body);

            _logger.Log($"Email sent: {result}");

            return Ok(new
            {
                success = true,
                tier = factory.GetFactoryType(),
                type = "Email",
                result = result,
                timestamp = DateTime.Now
            });
        }

        [HttpPost("send-sms")]
        public IActionResult SendSMS([FromBody] SMSRequest request)
        {
            _logger.Log($"Sending SMS via {request.NotificationTier} tier");

            // Select appropriate factory based on tier
            INotificationFactory factory = GetFactory(request.NotificationTier);
            
            // Create SMS notification using the factory
            ISMSNotification smsNotification = factory.CreateSMSNotification();
            string result = smsNotification.SendSMS(request.PhoneNumber, request.Message);

            _logger.Log($"SMS sent: {result}");

            return Ok(new
            {
                success = true,
                tier = factory.GetFactoryType(),
                type = "SMS",
                result = result,
                timestamp = DateTime.Now
            });
        }

        [HttpPost("send-both")]
        public IActionResult SendBoth([FromBody] BothNotificationRequest request)
        {
            _logger.Log($"Sending both Email and SMS via {request.NotificationTier} tier");

            // Abstract Factory creates a family of related products
            INotificationFactory factory = GetFactory(request.NotificationTier);
            
            IEmailNotification emailNotification = factory.CreateEmailNotification();
            ISMSNotification smsNotification = factory.CreateSMSNotification();

            string emailResult = emailNotification.SendEmail(request.Email, request.Subject, request.EmailBody);
            string smsResult = smsNotification.SendSMS(request.PhoneNumber, request.SMSMessage);

            _logger.Log($"Both notifications sent successfully");

            return Ok(new
            {
                success = true,
                tier = factory.GetFactoryType(),
                emailResult = emailResult,
                smsResult = smsResult,
                timestamp = DateTime.Now
            });
        }

        [HttpGet("tiers")]
        public IActionResult GetAvailableTiers()
        {
            return Ok(new
            {
                availableTiers = new[] { "Standard", "Premium" },
                description = "Use Abstract Factory to create notification families"
            });
        }

        private INotificationFactory GetFactory(string tier)
        {
            return tier.ToLower() switch
            {
                "premium" => new PremiumNotificationFactory(),
                "standard" => new StandardNotificationFactory(),
                _ => new StandardNotificationFactory()
            };
        }
    }

    public class EmailRequest
    {
        public string NotificationTier { get; set; } = "Standard";
        public string Recipient { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }

    public class SMSRequest
    {
        public string NotificationTier { get; set; } = "Standard";
        public string PhoneNumber { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    public class BothNotificationRequest
    {
        public string NotificationTier { get; set; } = "Standard";
        public string Email { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string EmailBody { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;
    }
}
