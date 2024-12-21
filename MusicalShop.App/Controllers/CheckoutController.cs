namespace MusicalShop.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using global::Stripe;
    using MusicalShop.Web.InputModels.Checkout;
    using MusicalShop.Services;
    using AutoMapper;
    using System.Linq;
    using MusicalShop.Services.Models;

    public class CheckoutController : Controller
    {
        private readonly IOrderService orderService;

        public CheckoutController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Charge(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = await customers.CreateAsync(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = await charges.CreateAsync(new ChargeCreateOptions
            {
                Amount = 500, // Amount in cents, replace with actual cart total
                Description = "Sample Charge",
                Currency = "usd",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                // Clear the cart after successful payment
                var cookies = Request.Cookies.Where(x => x.Key.StartsWith("product")).ToList();
                foreach (var cookie in cookies)
                {
                    Response.Cookies.Delete(cookie.Key);
                }

                // Create and save the order
                var order = new OrderServiceModel
                {
                    Email = stripeEmail,
                    OrderStatus = new OrderStatusServiceModel { Name = "Paid" },
                    IsCashOnDelivery = false,
                    // Add other necessary order details
                };
                await orderService.Create(order);

                // Redirect to the success page
                return RedirectToAction("Success", "Order");
            }

            // If payment failed, add an error message and redirect back to the cart
            TempData["ErrorMessage"] = "Payment failed. Please try again.";
            return RedirectToAction("Cart", "Cart");
        }
    }
}