using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.ResponseType;
using _300Shine.Service.Slots;
using Microsoft.AspNetCore.Mvc;

namespace _300Shine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : Controller
    {
        private readonly ISlotService _slotService;

        public ManagerController(ISlotService slotService)
        {
            _slotService = slotService;
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
