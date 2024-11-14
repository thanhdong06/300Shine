using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.ResponseType;
using _300Shine.Service.Styles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _300Shine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StylesController : ControllerBase
    {
        private IStyleService _styleService;

        public StylesController(IStyleService styleService)
        {
            _styleService = styleService;
        }
        [HttpGet("list")]
        public async Task<ActionResult<List<JsonResponse<StyleResponseDTO>>>> GetStyles(string? search,int pageIndex, int pageSize)
        {
            try
            {
                var result = await _styleService.GetAllStyles(search, pageIndex, pageSize);
                return Ok(new JsonResponse<List<StyleResponseDTO>>(result, 200, "Styles Retrieved Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something wrong, please contact with admin", 400, ex.Message));
            }
        }
    }
}
