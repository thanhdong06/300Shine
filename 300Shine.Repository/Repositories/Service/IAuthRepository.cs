using _300Shine.BusinessObject.DTO.Request;
using _300Shine.DataAccessLayer.DTO.RequestModel;
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
        Task<string> LoginAsync(LoginRequest request);
    }
}
