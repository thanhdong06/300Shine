using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.ResponseType;
using _300Shine.Service.Appoinments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using System.Security.Claims;
using _300Shine.DataAccessLayer.Entities;
using _300Shine.DataAccessLayer.DTO.ResponseModel;

namespace _300Shine.Controllers
{
    [ApiController]
    [Route("api/appointment")]
    public class CustomerController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public CustomerController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<JsonResponse<string>>> CreateAppointment([FromBody] AppointmentCreateDTO request)
        {
            try
            {
                var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return BadRequest(new JsonResponse<string>("User ID not found", 400, ""));
                }
                int userId = int.Parse(userIdClaim.Value);

                var genOrderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));

                var result = await _appointmentService.CreateAppointmentAsync(request, userId, genOrderCode);
                if (result == null)
                {
                    return BadRequest(new JsonResponse<string>("Failed to create appointment", 400, ""));
                }
                int amountInInt = Convert.ToInt32(Math.Round(result.Amount));

                var clientId = "38bb31de-35a1-4335-8bfa-34ab42934b0a";
                var apiKey = "4d398076-e456-42ab-8ced-149bdce1eb0e";
                var checksumKey = "2067a941fc37077fc1972209419726845f1db43072a0a971ae2169dd0df41e74";

                var payOS = new PayOS(clientId, apiKey, checksumKey);

                var paymentLinkRequest = new PaymentData(
                    orderCode: genOrderCode,
                    amount: amountInInt,
                    description: "Thanh toan don hang",
                    items: [],
                    returnUrl: "http://localhost:3039/payment-successfully",
                    cancelUrl: "http://localhost:3039/payment-cancel"
                );
                var response = await payOS.createPaymentLink(paymentLinkRequest);

                return Ok(new JsonResponse<object>(response, 400, "create payment request successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something wrong, please contact admin", 400, ex.Message));
            }
        }
        //[Authorize]
        [HttpPost("appoinment")]
        public async Task<ActionResult<JsonResponse<string>>> CreateAppointmentDetailWithReturnDayAsync(AppointmentDetailCreateWithReturnDateRequest request)
        {
            try
            {


                var result = await _appointmentService.CreateAppointmentDetailWithReturnDayAsync(request);
                if (result == null)
                {
                    return BadRequest(new JsonResponse<string>("Failed to create appointment", 400, ""));
                }

                return Ok(new JsonResponse<string>(null, 200, "Create Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something wrong, please contact admin", 400, ex.Message));
            }
        }
        
    }
}
