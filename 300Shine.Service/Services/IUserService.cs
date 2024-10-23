using _300Shine.BusinessObject.DTO.Request;
using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Service.Interface
{
    public interface IUserService
    {
        Task<List<ResponseUser>> GetAllUsersAsync(int? roleId = null);
        Task<ResponseUser> GetUserByPhoneAsync(string phone);
        Task<ResponseUser> GetUserByIdAsync(int userId);
        Task<string> CreateStylistAsync(CreateUserRequest request);
        Task<string> CreateManagerAsync(CreateUserRequest request);
        Task<string> UpdateUserAsync(int userId, UpdateUserRequest request);
        Task<string> DeleteUserAsync(int userId);
    }
}
