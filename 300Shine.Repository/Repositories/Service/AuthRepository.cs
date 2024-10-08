﻿using _300Shine.BusinessObject.DTO.Request;
using _300Shine.DataAccessLayer.DBContext;
using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Repository.Repositories.Service
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Register(RegisterRequest request)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Phone == request.Phone);
            if (existingUser != null)
            {
                throw new InvalidDataException("Phone number already exists");
            }

            var newUser = new UserEntity()
            {
                FullName = request.FullName,
                Password = request.Password,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Phone = request.Phone,
                Address = request.Address,
                RoleId = 3,
                SalonId = 1,
                IsVerified = false,
                Status = "Active",
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return "Register successfully";
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Phone == request.Phone);

            if (user == null)
            {
                throw new InvalidDataException("Invalid phone number or password");
            }

            if (user.Password != request.Password)
            {
                throw new InvalidDataException("Invalid phone number or password");
            }
            return "Login successful";
        }
    }
}
