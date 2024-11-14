using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.ResponseType;
using _300Shine.Service.Appoinments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _300Shine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        [Authorize]
        [HttpGet("list")]
        public async Task<ActionResult<JsonResponse<List<AppointmentResponseModel>>>> GetAppoinmentByUserId(string status, string appoinmentDetailStatus)
        {
            try
            {
                var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return BadRequest(new JsonResponse<string>("User ID not found", 400, ""));
                }
                int userId = int.Parse(userIdClaim.Value);

                var result = await _appointmentService.GetAppoinmentByUserId(userId, status, appoinmentDetailStatus);
                if (result == null)
                {
                    return BadRequest(new JsonResponse<string>("Failed to get appointments", 400, ""));
                }

                return Ok(new JsonResponse<List<AppointmentResponseModel>>(result, 200, "Get appointments successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something wrong, please contact admin", 400, ex.Message));
            }
        }

        //[Authorize]
        [HttpGet("list-by-status")]
        public async Task<ActionResult<JsonResponse<List<AppointmentResponseModel>>>> GetAppoinmentByStatus(string status)
        {
            try
            {
                var result = await _appointmentService.GetAppoinmentsByStatus(status);
                if (result == null)
                {
                    return BadRequest(new JsonResponse<string>("Failed to get appointments", 400, ""));
                }

                return Ok(new JsonResponse<List<AppointmentResponseModel>>(result, 200, "Get appointments successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something wrong, please contact admin", 400, ex.Message));
            }
        }
        [HttpGet("list-by-stylist")]
        public async Task<ActionResult<JsonResponse<List<AppointmentResponseModel>>>> GetAppoinmentByStylistId(string status, string appoinmentDetailStatus)
        {
            try
            {
                var stylistIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (stylistIdClaim == null)
                {
                    return BadRequest(new JsonResponse<string>("Stylist ID not found", 400, ""));
                }
                int stylistId = int.Parse(stylistIdClaim.Value);

                var result = await _appointmentService.GetAppoinmentByStylistId(stylistId, status, appoinmentDetailStatus);
                if (result == null)
                {
                    return BadRequest(new JsonResponse<string>("Failed to get appointments", 400, ""));
                }

                return Ok(new JsonResponse<List<AppointmentResponseModel>>(result, 200, "Get appointments successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something wrong, please contact admin", 400, ex.Message));
            }
        }
    }
}
