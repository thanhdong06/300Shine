using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.ResponseType;
using _300Shine.Service.Appoinments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _300Shine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

                var result = await _appointmentService.CreateAppointmentAsync(request, userId);
                if (result == null)
                {
                    return BadRequest(new JsonResponse<string>("Failed to create appointment", 400, ""));
                }

                return Ok(new JsonResponse<string>(null, 200, "Create Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("", 400, ex.Message));
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
                return BadRequest(new JsonResponse<string>("", 400, ex.Message));
            }
        }
    }
}
