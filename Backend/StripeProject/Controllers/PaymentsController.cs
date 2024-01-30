using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;

namespace StripeProject.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class PaymentsController : ControllerBase
  {
    private readonly IConfiguration _configuration;

    public PaymentsController(IConfiguration configuration)
    {
      _configuration = configuration;
      StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
    }

    [HttpPost("create-payment-intent")]
    public ActionResult<string> CreatePaymentIntent()
    {
      var service = new PaymentIntentService();
      var options = new PaymentIntentCreateOptions
      {
        Amount = 1000,  // Amount in cents
        Currency = "usd",
        PaymentMethodTypes = new List<string> { "card" },
      };

      try
      {
        var intent = service.Create(options);
        return Ok(new { clientSecret = intent.ClientSecret });
      }
      catch (Exception ex)
      {
        return BadRequest(new { error = ex.Message });
      }
    }
  }
}
