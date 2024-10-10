using _300Shine.BusinessObject.DTO.Request;
using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.Entities;
using _300Shine.Repository;
using _300Shine.Repository.Interface;
using _300Shine.Repository.Repositories.Service;
using _300Shine.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Service
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthRepository _authRepository;

        public AuthService(IConfiguration configuration, IAuthRepository authRepository) { 
            _configuration = configuration;
            _authRepository = authRepository;

        }
        public string CreateToken(LoginRequest user)
        {
            if (user == null || user.Phone <= 0)
            {
                throw new ArgumentException("Invalid login request", nameof(user));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:secret"]);
            var claims = new[]
            {   
                new Claim(ClaimTypes.NameIdentifier, user.Phone.ToString()),
                new Claim(ClaimTypes.Name, user.Phone.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> RegisterUserAsync(RegisterRequest registerRequest)
        {
            return await _authRepository.Register(registerRequest);
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var user = await _authRepository.LoginAsync(request);

            if (user == null)
            {
                throw new InvalidDataException("Invalid phone number or password");
            }
            return CreateToken(request);
        }
    }
}
