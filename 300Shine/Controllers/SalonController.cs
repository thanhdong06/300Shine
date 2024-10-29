using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.Entities;
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
        [HttpGet("salon/service-list")]
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

        [HttpGet("salon/salon-by-id")]
        public async Task<ActionResult<JsonResponse<SalonResponseModel>>> GetSalonID(int id)
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
        

        [HttpPost("salon/new")]
        public async Task<ActionResult<JsonResponse<string>>> CreateSalon([FromBody] SalonCreateDTO salon)
        {
            try
            {
                var result = await _service.CreateSalon(salon);
                return Ok(new JsonResponse<string>(null, 200, "Add Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong, please contact the admin", 400, ex.Message));
            }
        }

        [HttpPut("salon")]
        public async Task<ActionResult<JsonResponse<string>>> UpdateSalon(
            [FromBody] SalonUpdateDTO salon)
        {
            try
            {
                var result = await _service.UpdateSalon(salon);
                return Ok(new JsonResponse<string>(null, 200, "Update Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong, please contact the admin", 400, ex.Message));
            }
        }

        [HttpGet("salon/list")]
        public async Task<ActionResult<JsonResponse<List<SalonChoiceDTO>>>> GetSalonsForChoosing(string? search, string? sortBy,
            string? district, string? address, int pageIndex, int pageSize)
        {
            try
            {
                var result = await _service.GetSalonsForChoosing(search, sortBy, district, address, pageIndex, pageSize);
                return Ok(new JsonResponse<List<SalonChoiceDTO>>(result, 200, "Get Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong, please contact the admin", 400, ex.Message));
            }
        }

        [HttpDelete("salon")]
        public async Task<ActionResult<JsonResponse<string>>> DeleteSalon(int id)
        {
            try
            {
                var result = await _service.DeleteSalon(id);
                return Ok(new JsonResponse<string>(null, 200, "Delete Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong, please contact the admin", 400, ex.Message));
            }
        }
    }

}

