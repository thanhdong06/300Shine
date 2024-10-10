using _300Shine.BusinessObject.DTO.Request;
using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Repository.Interface
{
    public interface IUserRepository
    {
        Task<bool> PhoneExistsAsync(int phone);
        Task<UserEntity> GetUserByPhoneAsync(int phone);

        Task<List<ResponseUser>> GetAllUsersAsync();
        Task<ResponseUser> GetUserByIdAsync(int userId);
        Task<string> CreateUserAsync(CreateUserRequest request);
        Task<string> UpdateUserAsync(int userId, UpdateUserRequest request);
        Task<string> DeleteUserAsync(int userId);
    }
}
