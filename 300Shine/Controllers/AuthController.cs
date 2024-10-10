using _300Shine.BusinessObject.DTO.Request;
using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.Entities;
using _300Shine.ResponseType;
using _300Shine.Service;
using _300Shine.Service.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;

namespace _300Shine.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _autheService;

        public AuthController(IAuthService autheService)
        {
            _autheService = autheService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<JsonResponse<string>>> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var result = await _autheService.RegisterUserAsync(request);
                return Ok(new JsonResponse<string>(null, 200, "Register successfully"));
            }        
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong, please contact admin", 400, ex.Message));
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<JsonResponse<string>>> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _autheService.LoginAsync(request);
                return Ok(new JsonResponse<string>(null, 200, token));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong, please contact admin", 400, ex.Message));
            }
        }

    }
}
