using _300Shine.DataAccessLayer.DBContext;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Repository.Repositories.Manager
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly AppDbContext _context;

        public ManagerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StylistResponseModel>> GetAvailableStylistsAsync(int salonId, int serviceId, DateTime date)
        {
            // Stylist theo salon
            var salonStylists = await _context.Stylists
                .Where(s => s.SalonId == salonId)
                .Select(s => s.UserId)
                .ToListAsync();

            // Stylist theo service
            var serviceStylists = await _context.ServiceStyles
                .Where(ss => ss.Id == serviceId)
                .Select(ss => ss.StyleId)
                .ToListAsync();

            // So sánh
            var availableStylists = salonStylists.Intersect(serviceStylists).ToList();

            if (!availableStylists.Any())
            {
                throw new InvalidOperationException("Salon hiện không có stylist nào làm được dịch vụ này");
            }

            // check stylist available on Date
            var availableOnDate = await _context.StylistShifts
                .Where(s => availableStylists.Contains(s.StylistId) && s.Shift.Date == date)
                .Select(s => s.StylistId)
                .ToListAsync();

            if (!availableOnDate.Any())
            {
                throw new InvalidOperationException($"Không có stylist làm được dịch vụ này vào ngày {date.ToShortDateString()}");
            }

            // Fetch the stylist details
            var stylistResponses = await _context.Users
                .Where(u => availableOnDate.Contains(u.Id))
                .Select(u => new StylistResponseModel
                {                  
                    Id = u.Id,
                    Name = u.FullName,
                    ImageUrl = u.ImageUrl,

                })
                .ToListAsync();

            return stylistResponses;
        }
    }
}
