using Microsoft.AspNetCore.Mvc;
using Lab01.Patterns.Builder;
using Lab01.Services;

namespace Lab01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILoggerService _logger;

        public OrderController(ILoggerService logger)
        {
            _logger = logger;
        }

        [HttpPost("create-custom")]
        public IActionResult CreateCustomOrder([FromBody] CustomOrderRequest request)
        {
            try
            {
                _logger.Log($"Building custom order for customer: {request.CustomerName}");

                IOrderBuilder builder = new OrderBuilder();
                
                builder.SetCustomerInfo(request.CustomerName, request.CustomerEmail, request.CustomerPhone);

                if (!string.IsNullOrEmpty(request.ShippingAddress))
                    builder.SetShippingAddress(request.ShippingAddress);

                if (!string.IsNullOrEmpty(request.BillingAddress))
                    builder.SetBillingAddress(request.BillingAddress);

                foreach (var item in request.Items)
                {
                    builder.AddItem(item.ProductName, item.Price, item.Quantity);
                }

                builder.SetPaymentMethod(request.PaymentMethod)
                       .SetShippingFee(request.ShippingFee)
                       .SetTax(request.Tax);

                if (request.Discount > 0)
                    builder.SetDiscount(request.Discount);

                if (!string.IsNullOrEmpty(request.CouponCode))
                    builder.SetCoupon(request.CouponCode);

                if (!string.IsNullOrEmpty(request.Notes))
                    builder.SetNotes(request.Notes);

                if (request.IsGift)
                    builder.SetAsGift(request.GiftMessage);

                Order order = builder.Build();

                _logger.Log($"Order created successfully: {order}");

                return Ok(new
                {
                    success = true,
                    order = new
                    {
                        order.OrderId,
                        order.CustomerName,
                        order.CustomerEmail,
                        order.Items,
                        subtotal = order.GetSubtotal(),
                        order.ShippingFee,
                        order.Tax,
                        order.Discount,
                        total = order.GetTotal(),
                        order.PaymentMethod,
                        order.Status,
                        order.IsGift,
                        order.OrderDate
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Log($"Failed to create order: {ex.Message}");
                return BadRequest(new { success = false, error = ex.Message });
            }
        }

        [HttpPost("create-standard")]
        public IActionResult CreateStandardOrder([FromBody] SimpleOrderRequest request)
        {
            _logger.Log($"Creating standard order using Director");

            var director = new OrderDirector();
            var builder = new OrderBuilder();
            
            Order order = director.CreateStandardOrder(builder, request.CustomerName, request.CustomerEmail);

            _logger.Log($"Standard order created: {order}");

            return Ok(new
            {
                success = true,
                type = "Standard Order",
                order = GetOrderResponse(order)
            });
        }

        [HttpPost("create-premium")]
        public IActionResult CreatePremiumOrder([FromBody] PremiumOrderRequest request)
        {
            _logger.Log($"Creating premium order using Director");

            var director = new OrderDirector();
            var builder = new OrderBuilder();
            
            Order order = director.CreatePremiumOrder(builder, request.CustomerName, request.CustomerEmail, request.CustomerPhone);

            _logger.Log($"Premium order created: {order}");

            return Ok(new
            {
                success = true,
                type = "Premium Order",
                order = GetOrderResponse(order)
            });
        }

        [HttpPost("create-gift")]
        public IActionResult CreateGiftOrder([FromBody] GiftOrderRequest request)
        {
            _logger.Log($"Creating gift order using Director");

            var director = new OrderDirector();
            var builder = new OrderBuilder();
            
            Order order = director.CreateGiftOrder(builder, request.SenderName, request.RecipientAddress, request.GiftMessage);

            _logger.Log($"Gift order created: {order}");

            return Ok(new
            {
                success = true,
                type = "Gift Order",
                order = GetOrderResponse(order)
            });
        }

        private object GetOrderResponse(Order order)
        {
            return new
            {
                order.OrderId,
                order.CustomerName,
                order.CustomerEmail,
                order.CustomerPhone,
                order.ShippingAddress,
                items = order.Items.Select(i => new
                {
                    i.ProductName,
                    i.Price,
                    i.Quantity,
                    total = i.Price * i.Quantity
                }),
                subtotal = order.GetSubtotal(),
                order.ShippingFee,
                order.Tax,
                order.Discount,
                order.CouponCode,
                total = order.GetTotal(),
                order.PaymentMethod,
                order.IsGift,
                order.GiftMessage,
                order.Notes,
                order.Status,
                order.OrderDate
            };
        }
    }

    public class CustomOrderRequest
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string BillingAddress { get; set; } = string.Empty;
        public List<OrderItemRequest> Items { get; set; } = new List<OrderItemRequest>();
        public string PaymentMethod { get; set; } = "Cash";
        public decimal ShippingFee { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public bool IsGift { get; set; }
        public string GiftMessage { get; set; } = string.Empty;
    }

    public class OrderItemRequest
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class SimpleOrderRequest
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
    }

    public class PremiumOrderRequest
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
    }

    public class GiftOrderRequest
    {
        public string SenderName { get; set; } = string.Empty;
        public string RecipientAddress { get; set; } = string.Empty;
        public string GiftMessage { get; set; } = string.Empty;
    }
}
