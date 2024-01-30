using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using StripeProject.Models; // Ensure you have the correct namespace for the Product class
using System;
using System.Collections.Generic;

namespace StripeProject.Controllers;

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

  private static readonly List<ProductItem> ProductList = new List<ProductItem>
{
    new ProductItem { Id = 1, Name = "Product 1", Price = 1000 },
    new ProductItem { Id = 2, Name = "Product 2", Price = 1500 },
    new ProductItem { Id = 3, Name = "Product 3", Price = 2000 },
};

  [HttpGet("get-product")]
  public ActionResult<IEnumerable<Product>> GetProduct()
  {
    return Ok(ProductList);
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
