using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.ResponseType;
using _300Shine.Service.Salons;
using _300Shine.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace _300Shine.Controllers
{
    [Route("api/v1")]
    [ApiController]
    //[Authorize]
    public class SalonController : Controller
    {



        private readonly ISalonService _service;

        public SalonController(ISalonService service)
        {
            _service = service;
        }
        [HttpGet("salon/list")]
        public async Task<ActionResult<List<JsonResponse<SalonResponseModel>>>> GetServices(string? search, string? sortBy,
            decimal? fromPrice, decimal? toPrice,
            int? flavorID, string? size, int? typeID,
            int pageIndex, int pageSize)
        {
            try
            {
                var result = await _service.GetSalons(search, sortBy, fromPrice, toPrice, size, pageIndex, pageSize);
                return Ok(new JsonResponse<List<SalonResponseModel>>(result, 200, "Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something wrong, please contact with admin", 400, ex.Message));
            }
        }

        [HttpGet("salon/service-by-id")]
        public async Task<ActionResult<JsonResponse<SalonResponseModel>>> GetHotPotByID(int id)
        {
            try
            {
                var result = await _service.GetSalonByID(id);
                return Ok(new JsonResponse<SalonResponseModel>(result, 200, "Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something wrong, please contact with admin", 400, ex.Message));
            }

        }
    }

}
