using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;

namespace _300Shine.Controllers
{
    public class PaymentController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var clientId = "YOUR_CLIENT_ID";
            var apiKey = "YOUR_API_KEY";
            var checksumKey = "YOUR_CHECKSUM_KEY";

            var payOS = new PayOS(clientId, apiKey, checksumKey);

            var paymentLinkRequest = new PaymentData(
                orderCode: int.Parse(DateTimeOffset.Now.ToString("ffffff")),
                amount: 2000,
                description: "Thanh toan don hang",
                items: [],
                returnUrl:   "",
                cancelUrl:   ""
            );
            var response = await payOS.createPaymentLink(paymentLinkRequest);

            Response.Headers.Append("Location", response.checkoutUrl);
            return new StatusCodeResult(303);
        }
    }
}
