using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.ResponseType;
using _300Shine.Service.Interface;
using _300Shine.Service.Manager;
using _300Shine.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace _300Shine.Controllers
{
    [ApiController]
    [Route("api/manager")]
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
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
    }
}
