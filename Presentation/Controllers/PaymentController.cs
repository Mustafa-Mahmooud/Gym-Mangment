using Microsoft.AspNetCore.Mvc;
using Stripe;
using Presentation.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration)
        {
            
            _configuration = configuration;

        }

        [HttpPost("create-payment-intent")]
        public ActionResult CreatePaymentIntent([FromBody] PaymentIntentCreateRequest request)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            var options = new PaymentIntentCreateOptions
            {
                Amount = request.Amount,
                Currency = request.Currency,
                PaymentMethodTypes = new List<string> { "card" },
            };
            var service = new PaymentIntentService();
            PaymentIntent intent = service.Create(options);

            return Ok(new { clientSecret = intent.ClientSecret });
        }
    }

    public class PaymentIntentCreateRequest
    {
        public long Amount { get; set; }
        public string Currency { get; set; }
    }
}
