using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.ResponseType;
using _300Shine.Service.Appoinments;
using _300Shine.Service.Manager;
using _300Shine.Service.Services;

using _300Shine.Service.Slots;
using Microsoft.AspNetCore.Mvc;

namespace _300Shine.Controllers
{
    [ApiController]
    [Route("api/manager")]
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly ISlotService _slotService;
        private readonly IAppointmentService _appointmentService;

        public ManagerController(IManagerService managerService, ISlotService slotService, IAppointmentService appointmentService)
        {
            _managerService = managerService;
            _slotService = slotService;
            _appointmentService = appointmentService;
        }

        [HttpGet("available-stylists")]
        public async Task<ActionResult<JsonResponse<List<StylistResponseModel>>>> GetAvailableStylists(int salonId, int serviceId, DateTime date)
        {
            try
            {
                var availableStylists = await _managerService.GetAvailableStylistsAsync(salonId, serviceId, date);
                return Ok(new JsonResponse<List<StylistResponseModel>>(availableStylists, 200, "Successfully."));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message, 400, ""));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong, please contact admin.", 400, ex.Message));
            }
        }

        [HttpGet("slot-by-stylistId-date")]
        public async Task<ActionResult<JsonResponse<List<SlotResponseModel>>>> GetSlotByStylistIdAndDate(int stylistId, DateTime date)
        {
            try
            {
                var result = await _slotService.GetSlotByStylistIdAndDate(stylistId, date);
                return Ok(new JsonResponse<List<SlotResponseModel>>(result, 200, "Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something wrong, please contact with admin", 400, ex.Message));
            }
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateAppointmentStatusRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Status))
            {
                return BadRequest("Invalid request data.");
            }
            try
            {
                var updatedAppointment = await _appointmentService.UpdateAppointmentStatusAsync(request.OrderCode, request.Status);
                return Ok(updatedAppointment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpPut("update-status-by-ID")]
        public async Task<IActionResult> UpdateStatusByID([FromBody] UpdateAppointmentStatusByID request)
        {
            if (request == null || string.IsNullOrEmpty(request.Status))
            {
                return BadRequest("Invalid request data.");
            }
            try
            {
                var updatedAppointment = await _appointmentService.UpdateAppointmentById(request.AppointmentDetailId, request.Status);
                return Ok(updatedAppointment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
