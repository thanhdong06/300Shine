using _300Shine.BusinessObject.DTO.Request;
using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.Entities;
using _300Shine.ResponseType;
using _300Shine.Service;
using _300Shine.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _300Shine.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        public async Task<ActionResult<JsonResponse<List<ResponseUser>>>> GetAllUsers([FromQuery] int? roleId = null)
        {
            try
            {
                var users = await _userService.GetAllUsersAsync(roleId);
                return Ok(new JsonResponse<List<ResponseUser>>(users, 200, "Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<List<ResponseUser>>(null, 400, ex.Message));
            }
        }

        [HttpGet("user/by/{phone}")]
        public async Task<ActionResult<JsonResponse<ResponseUser>>> GetUserByPhoneAsync(string phone)
        {
            try
            {
                var user = await _userService.GetUserByPhoneAsync(phone);
                return Ok(new JsonResponse<ResponseUser>(user, 200, "Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<ResponseUser>(null, 400, ex.Message));
            }
        } 
        
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<JsonResponse<ResponseUser>>> GetUserByIdAsync(int userId)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(userId);
                return Ok(new JsonResponse<ResponseUser>(user, 200, "Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<ResponseUser>(null, 400, ex.Message));
            }
        }

        [HttpPost("stylist/create")]
        public async Task<ActionResult<JsonResponse<string>>> CreateStylist([FromBody] CreateUserRequest request)
        {
            try
            {
                var result = await _userService.CreateStylistAsync(request);
                if (result == "Role not found")
                {
                    return BadRequest(new JsonResponse<string>("Role not found", 400, ""));
                }

                if (result == "Salon not found")
                {
                    return BadRequest(new JsonResponse<string>("Salon not found", 400, ""));
                }

                if (result == "User with this phone number already exists")
                {
                    return BadRequest(new JsonResponse<string>("Phone number already exists", 400, ""));
                }

                return Ok(new JsonResponse<string>(null, 200, "Create Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("", 400, ex.Message));
            }
        } 
        
        [HttpPost("manager/create")]
        public async Task<ActionResult<JsonResponse<string>>> CreateManager([FromBody] CreateUserRequest request)
        {
            try
            {
                var result = await _userService.CreateManagerAsync(request);
                if (result == "Role not found")
                {
                    return BadRequest(new JsonResponse<string>("Role not found", 400, ""));
                }

                if (result == "Salon not found")
                {
                    return BadRequest(new JsonResponse<string>("Salon not found", 400, ""));
                }

                if (result == "User with this phone number already exists")
                {
                    return BadRequest(new JsonResponse<string>("Phone number already exists", 400, ""));
                }

                return Ok(new JsonResponse<string>(null, 200, "Create Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("", 400, ex.Message));
            }
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult<JsonResponse<string>>> UpdateUser(int userId, [FromBody] UpdateUserRequest request)
        {
            try
            {
                var result = await _userService.UpdateUserAsync(userId, request);
                return Ok(new JsonResponse<string>(result, 200, "Update Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong", 400, ex.Message));
            }
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<JsonResponse<string>>> DeleteUser(int userId)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(userId);
                return Ok(new JsonResponse<string>(result, 200, ""));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong", 400, ex.Message));
            }
        }
    }
}
