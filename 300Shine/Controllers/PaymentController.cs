using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using _300Shine.ResponseType;

namespace _300Shine.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Create(int orderCode, int amount)
        {
            var clientId = "38bb31de-35a1-4335-8bfa-34ab42934b0a";
            var apiKey = "4d398076-e456-42ab-8ced-149bdce1eb0e";
            var checksumKey = "2067a941fc37077fc1972209419726845f1db43072a0a971ae2169dd0df41e74";

            var payOS = new PayOS(clientId, apiKey, checksumKey);

            var paymentLinkRequest = new PaymentData(
                orderCode: orderCode,
                amount: amount,
                description: "Thanh toan don hang",
                items: [],
                returnUrl: "http://localhost:3039/payment-successfully",
                cancelUrl: "http://localhost:3039/payment-cancel"
            );
            var response = await payOS.createPaymentLink(paymentLinkRequest);

            return Ok(new JsonResponse<object>(response, 400, "create payment request successfully"));
        }
    }
}
