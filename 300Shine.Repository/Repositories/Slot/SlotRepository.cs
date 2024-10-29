using _300Shine.DataAccessLayer.DBContext;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Repository.Repositories.Slot
{
    public class SlotRepository : ISlotRepository
    {
        private readonly AppDbContext _context = new();

        private readonly IMapper _mapper;

        public async Task<List<SlotResponseModel>> GetSlotByStylistIdAndDate(int stylistId, DateTime date)
        {
           var stylist = await _context.Stylists.SingleOrDefaultAsync(s => s.Id == stylistId);
            if (stylist == null)
                throw new Exception("stylidt not found");
            var stylistShifts = await _context.StylistShifts.Include(sh=>sh.Shift)
                .Where(x => x.StylistId == stylistId
            && x.Shift.Date.Year == date.Year
            && x.Shift.Date.Month == date.Month
            && x.Shift.Date.Day == date.Day)
                .Select(x => x.Shift).ToListAsync();

            if (stylistShifts == null || !stylistShifts.Any())
                throw new Exception("Stylist has not chosen any shifts on this date");

           
            var slots = await _context.Slots
                .Where(x => !x.IsDeleted)
                .ToListAsync();           
            var appDetails = await _context.AppointmentDetails
                .Where(s => s.StylistId == stylistId 
                && s.Appointment.Date.Year == date.Year 
                && s.Appointment.Date.Month == date.Month 
                && s.Appointment.Date.Day == date.Day) // Filter by stylistId and appointment date, sửa so sánh ngày giờ  
                .ToListAsync();
            if (appDetails == null || !appDetails.Any())
                throw new Exception("Stylist of appoinment not found");


            // Initialize the list for available slots
            var availableSlots = new List<SlotResponseModel>();

            // Iterate through each slot to determine its availability
            foreach (var slot in slots)
            {
                // Check if the slot time falls within any of the stylist's shifts
                bool isWithinShift = stylistShifts.Any(shift => slot.Time.TimeOfDay >= shift.StartTime.TimeOfDay && slot.Time.TimeOfDay < shift.EndTime.TimeOfDay);

                // Get the list of slots already booked for the stylist's appointments on that day
                var bookedSlots = await _context.AppointmentSlots
                    .Where(x => appDetails.Select(ad => ad.Id)
                    .Contains(x.AppointmentDetailId) && x.SlotId == slot.Id)
                    .Select(x => x.SlotId)
                    .ToListAsync();
                var status = isWithinShift && !bookedSlots.Contains(slot.Id) ? true : false;

                availableSlots.Add(new SlotResponseModel
                {
                    Id = slot.Id,
                    Time = slot.Time,
                    Status = status // Set the correct status based on the conditions
                });

            }
            return availableSlots;

        }
    }
}
