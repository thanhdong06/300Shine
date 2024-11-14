using _300Shine.BusinessObject.DTO.Request;
using _300Shine.DataAccessLayer.DBContext;
using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.Entities;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace _300Shine.Repository.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> PhoneExistsAsync(string phone)
        {
            return await _context.Users.AnyAsync(u => u.Phone == phone);
        }

        public async Task<List<ResponseUser>> GetAllUsersAsync(int? roleId = null)
        {
            var query =  _context.Users.Include(u => u.Role).Include(u => u.Salon).Where(u => !u.IsDeleted);
            if (roleId.HasValue)
            {
                query = query.Where(u => u.RoleId == roleId.Value);
            }

            var users = await query.ToListAsync();
            var userResponses = new List<ResponseUser>();

            foreach (var user in users)
            {
                var userResponse = new ResponseUser
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender,
                    Phone = user.Phone,
                    Address = user.Address,
                    IsVerified = user.IsVerified,
                    Status = user.Status,
                    SalonId = user.SalonId,
                    RoleName = user.Role.Name,
                    ImageUrl = user.ImageUrl,
                };

                var stylist = await _context.Stylists.FirstOrDefaultAsync(s => s.UserId == user.Id);
                if (stylist != null)
                {
                    userResponse.Commission = stylist.Commission;
                    userResponse.Salary = stylist.Salary;
                    userResponse.SalaryPerDay = stylist.SalaryPerDay;
                }

                userResponses.Add(userResponse);
            }

            return userResponses;
        }

        public async Task<ResponseUser> GetUserByPhoneAsync(string phone)
        {
            var user = await _context.Users.Include(u => u.Role).Include(u => u.Salon).FirstOrDefaultAsync(u => u.Phone == phone && !u.IsDeleted);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var userResponse = new ResponseUser
            {
                Id = user.Id,
                FullName = user.FullName,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Phone = user.Phone,
                Address = user.Address,
                IsVerified = user.IsVerified,
                Status = user.Status,
                SalonId = user.SalonId,
                RoleName = user.Role.Name

            };

            var stylist = await _context.Stylists.FirstOrDefaultAsync(s => s.UserId == user.Id);
            if (stylist != null)
            {
                userResponse.Commission = stylist.Commission;
                userResponse.Salary = stylist.Salary;
                userResponse.SalaryPerDay = stylist.SalaryPerDay;
            }
            return userResponse;
        } public async Task<ResponseUser> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.Include(u => u.Role).Include(u => u.Salon).FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var userResponse = new ResponseUser
            {
                Id = user.Id,
                FullName = user.FullName,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Phone = user.Phone,
                Address = user.Address,
                IsVerified = user.IsVerified,
                Status = user.Status,
                SalonId = user.SalonId,
                RoleName = user.Role.Name,
                ImageUrl = user.ImageUrl,
            };

            var stylist = await _context.Stylists.FirstOrDefaultAsync(s => s.UserId == user.Id);
            if (stylist != null)
            {
                userResponse.Commission = stylist.Commission;
                userResponse.Salary = stylist.Salary;
                userResponse.SalaryPerDay = stylist.SalaryPerDay;
            }
            return userResponse;
        }

        public async Task<string> CreateStylistAsync(CreateUserRequest request)
        {
            var checkUser = await _context.Users.FirstOrDefaultAsync(s => s.Phone == request.Phone && s.IsDeleted == false);
            if (checkUser != null)
            {
                throw new InvalidDataException("User with this phone number already exists");
            }

            var checkRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name.ToLower() == "stylist");
            if (checkRole == null) throw new InvalidDataException("Role not found");

            var newUser = new UserEntity()
            {
                ImageUrl = request.ImageUrl,
                FullName = request.FullName,
                Password = request.Password,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Phone = request.Phone,
                Address = request.Address,
                RoleId = checkRole.Id,
                IsVerified = request.IsVerified,
                Status = "Active",
                SalonId = request.SalonId
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var newStylist = new StylistEntity()
            {
                UserId = newUser.Id,
                Commission = request.Commission,
                Salary = request.Salary,
                SalaryPerDay = request.SalaryPerDay,
                SalonId = request.SalonId
            };

            _context.Stylists.Add(newStylist);
            await _context.SaveChangesAsync();

            if (request.StyleId != null && request.StyleId.Any())
            {
                var stylistStyles = request.StyleId.Select(styleId => new StylistStyleEntity
                {
                    StylistId = newStylist.Id,
                    StyleId = styleId
                });

                _context.StylistStyles.AddRange(stylistStyles);
                await _context.SaveChangesAsync();
            }

            return "Stylist created successfully";
          
        }  
        public async Task<string> CreateManagerAsync(CreateUserRequest request)
        {
            var checkUser = await _context.Users.FirstOrDefaultAsync(s => s.Phone == request.Phone && s.IsDeleted == false);
            if (checkUser != null)
            {
                throw new InvalidDataException("User with this phone number already exists");
            }

            var checkRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name.ToLower() == "manager");
            if (checkRole == null) throw new InvalidDataException("Role not found");
            
            var newUser = new UserEntity()
            {
                ImageUrl = request.ImageUrl,
                FullName = request.FullName,
                Password = request.Password,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Phone = request.Phone,
                Address = request.Address,
                RoleId = checkRole.Id,
                IsVerified = request.IsVerified,
                Status = "Active",
                SalonId = request.SalonId
            };

            return "Manager created successfully";          
        }

        public async Task<string> UpdateUserAsync(int userId, UpdateUserRequest request)
        {
            var existingUser = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == userId);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            existingUser.FullName = request.FullName;
            existingUser.DateOfBirth = request.DateOfBirth ?? existingUser.DateOfBirth;
            existingUser.Gender = request.Gender ?? existingUser.Gender;
            existingUser.Phone = request.Phone ?? request.Phone;
            existingUser.Address = request.Address;
            existingUser.RoleId = request.RoleId ?? existingUser.RoleId;
            existingUser.SalonId = request.SalonId ?? existingUser.SalonId;
            existingUser.IsVerified = request.IsVerified ?? existingUser.IsVerified;
            existingUser.Status = request.Status ?? existingUser.Status;
            _context.Users.Update(existingUser);

            var existingStylist = await _context.Stylists.FirstOrDefaultAsync(s => s.UserId == userId);

            if (request.IsStylist.HasValue && request.IsStylist.Value)
            {
                if (existingStylist != null)
                {
                    if (request.Commission.HasValue)
                        existingStylist.Commission = request.Commission.Value;

                    if (request.Salary.HasValue)
                        existingStylist.Salary = request.Salary.Value;

                    if (request.SalaryPerDay.HasValue)
                        existingStylist.SalaryPerDay = request.SalaryPerDay.Value;

                    _context.Stylists.Update(existingStylist);
                }
                else
                {
                    // Tạo stylist mới nếu không tồn tại
                    var newStylist = new StylistEntity()
                    {
                        UserId = userId,
                        Commission = request.Commission ?? 0,
                        Salary = request.Salary ?? 0,
                        SalaryPerDay = request.SalaryPerDay ?? 0,
                        SalonId = existingUser.SalonId
                    };
                    _context.Stylists.Add(newStylist);
                }
            }
            else if (existingStylist != null)
            {
                _context.Stylists.Remove(existingStylist);
            }

            await _context.SaveChangesAsync();

            return "User updated successfully";
        }

        public async Task<string> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            user.IsDeleted = true;
            await _context.SaveChangesAsync();

            return "User deleted successfully";
        }
    }
}
