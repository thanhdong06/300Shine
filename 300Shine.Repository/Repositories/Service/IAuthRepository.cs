using _300Shine.BusinessObject.DTO.Request;
using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Repository.Repositories.Service
{
    public interface IAuthRepository
    {
        Task<string> Register(RegisterRequest request);
        Task<UserEntity> LoginAsync(LoginRequest request);
        Task<UserEntity> GetUserByPhoneAsync(string phone);
        Task UpdateUserAsync(UserEntity user);
    }
}
