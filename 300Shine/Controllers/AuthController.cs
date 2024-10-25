using _300Shine.BusinessObject.DTO.Request;
using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.Entities;
using _300Shine.ResponseType;
using _300Shine.Service;
using _300Shine.Service.SMS;
using _300Shine.Service.Users;
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
        private readonly ISMSService _smsService;

        public AuthController(IAuthService autheService, ISMSService smsService)
        {
            _autheService = autheService;
            _smsService = smsService;
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

        [HttpPost]
        [Route("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            try
            {
                var result = await _autheService.VerifyOtpAsync(request);
                return Ok(new JsonResponse<string>(result, 200, "OTP verification process completed."));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong, please contact admin", 400, ex.Message));
            }
        }
    }
}
