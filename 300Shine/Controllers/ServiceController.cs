using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.ResponseType;
using _300Shine.Service.Services;
using AutoMapper;
using DataAccessLayer.ServiceForCRUD.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _300Shine.Controllers
{
    [Route("api/v1")]
    [ApiController]
    //[Authorize]
    public class ServiceController : Controller
    {
        private readonly IServiceEntityService _serviceService;

        public ServiceController(IServiceEntityService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpPost("service/new")]
        public async Task<ActionResult<JsonResponse<string>>> CreateService(
            [FromBody] CreateServiceRequestModel service)
        {
            try
            {
                var result = await _serviceService.CreateService(service);
                return Ok(new JsonResponse<string>(null, 200, "Create Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something wrong, please contact with admin", 400, ex.Message));
            }
        }

        [HttpPut("service")]
        public async Task<ActionResult<JsonResponse<string>>> UpdateService(
            [FromBody] UpdateServiceRequestModel service)
        {
            try
            {
                var result = await _serviceService.UpdateService(service);
                return Ok(new JsonResponse<string>(null, 200, "Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something wrong, please contact with admin", 400, ex.Message));
            }
        }

        [HttpGet("service/list")]
        public async Task<ActionResult<List<JsonResponse<ServiceResponseModel>>>> GetServices(string? search, string? sortBy,
            decimal? fromPrice, decimal? toPrice,
            int? flavorID, string? size, int? typeID,
            int pageIndex, int pageSize)
        {
            try
            {
                var result = await _serviceService.GetServices(search, sortBy, fromPrice, toPrice, size, pageIndex, pageSize);
                return Ok(new JsonResponse<List<ServiceResponseModel>>(result, 200, "Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something wrong, please contact with admin", 400, ex.Message));
            }
        }

        [HttpDelete("service")]
        public async Task<ActionResult<JsonResponse<string>>> DeleteHotPot(int id)
        {
            try
            {
                var result = await _serviceService.DeleteService(id);
                return Ok(new JsonResponse<string>(null, 200, "Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something wrong, please contact with admin", 400, ex.Message));
            }
        }

        [HttpGet("service/service-by-id")]
        public async Task<ActionResult<JsonResponse<ServiceResponseModel>>> GetHotPotByID(int id)
        {
            try
            {
                var result = await _serviceService.GetServiceByID(id);
                return Ok(new JsonResponse<ServiceResponseModel>(result, 200, "Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something wrong, please contact with admin", 400, ex.Message));
            }

        }

        [HttpGet("service/list-by-stylist")]
        public async Task<ActionResult<JsonResponse<PaginatedList<ServiceResponseForChooseStylistFirst>>>> GetServicesByStylist(int stylistId, int pageIndex, int pageSize)
        {
            try
            {
                var result = await _serviceService.GetServicesByStylist(stylistId, pageIndex, pageSize);
                return Ok(new JsonResponse<PaginatedList<ServiceResponseForChooseStylistFirst>>(result, 200, "Retrieve service list by stylist id successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something wrong, please contact with admin", 400, ex.Message));
            }

        }
    }
}
