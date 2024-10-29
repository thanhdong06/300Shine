using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.ResponseType;
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

        public ManagerController(IManagerService managerService, ISlotService slotService)
        {
            _managerService = managerService;
            _slotService = slotService;
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
    }
}
