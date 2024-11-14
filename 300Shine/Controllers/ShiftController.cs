using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.Entities;
using _300Shine.ResponseType;
using _300Shine.Service.Shifts;
using Microsoft.AspNetCore.Mvc;

namespace _300Shine.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpPost("shift/new")]
        public async Task<ActionResult<JsonResponse<string>>> CreateShift([FromBody] ShiftCreateDTO shift)
        {
            try
            {
                var result = await _shiftService.CreateShift(shift);
                return Ok(new JsonResponse<string>(null, 200, result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong, please contact admin", 400, ex.Message));
            }
        }

        [HttpPut("shift")]
        public async Task<ActionResult<JsonResponse<string>>> UpdateShift([FromBody] ShiftUpdateDTO shift)
        {
            try
            {
                var result = await _shiftService.UpdateShift(shift);
                return Ok(new JsonResponse<string>(null, 200, result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong, please contact admin", 400, ex.Message));
            }
        }

        [HttpDelete("shift")]
        public async Task<ActionResult<JsonResponse<string>>> DeleteShift(int id)
        {
            try
            {
                var result = await _shiftService.DeleteShift(id);
                return Ok(new JsonResponse<string>(null, 200, result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong, please contact admin", 400, ex.Message));
            }
        }

        [HttpGet("shift/list")]
        public async Task<ActionResult<JsonResponse<List<ShiftResponseDTO>>>> GetShifts(string? search, DateTime? date, string? status, int pageIndex, int pageSize)
        {
            try
            {
                var result = await _shiftService.GetShifts(search, date, status, pageIndex, pageSize);
                return Ok(new JsonResponse<List<ShiftResponseDTO>>(result, 200, "Successfully retrieved shifts"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong, please contact admin", 400, ex.Message));
            }
        }

        [HttpGet("shift")]
        public async Task<ActionResult<JsonResponse<ShiftResponseDTO>>> GetShiftById(int id)
        {
            try
            {
                var result = await _shiftService.GetShiftById(id);
                return Ok(new JsonResponse<ShiftResponseDTO>(result, 200, "Successfully retrieved shift"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong, please contact admin", 400, ex.Message));
            }
        }

        [HttpGet("shift/list-by-salon-id")]
        public async Task<ActionResult<JsonResponse<List<ShiftForChoosingDTO>>>> GetShiftsBySalonAndStylistId(int salonId, int stylistId)
        {
            try
            {
                var result = await _shiftService.GetShiftsBySalonAndStylistId(salonId, stylistId);
                return Ok(new JsonResponse<List<ShiftForChoosingDTO>>(result, 200, "Successfully retrieved shifts by salon id"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong, please contact admin", 400, ex.Message));
            }
        }

        [HttpPost("shift/new-shift-for-stylist")]
        public async Task<ActionResult<JsonResponse<string>>> CreateShiftForStylist([FromBody] ShiftCreateForStylistDTO request)
        {
            try
            {
                var result = await _shiftService.ShiftsForStylist(request);
                return Ok(new JsonResponse<string>("Shift chosen successfully", 200, result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong, please contact admin", 400, ex.Message));
            }
        }
    }

}
