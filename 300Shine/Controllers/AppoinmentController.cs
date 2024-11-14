using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.ResponseType;
using _300Shine.Service.Appoinments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _300Shine.Controllers
{
    [ApiController]
    [Route("api/appointment")]
    public class AppoinmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppoinmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        [Authorize]
        [HttpGet("list")]
        public async Task<ActionResult<JsonResponse<List<AppointmentResponseModel>>>> GetAppoinmentById(string status)
        {
            try
            {
                var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return BadRequest(new JsonResponse<string>("User ID not found", 400, ""));
                }
                int userId = int.Parse(userIdClaim.Value);

                var result = await _appointmentService.GetAppoinmentByUserId(userId, status);
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
    }
}
