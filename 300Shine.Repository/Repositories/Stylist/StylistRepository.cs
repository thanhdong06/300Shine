using _300Shine.DataAccessLayer.DBContext;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _300Shine.Repository.Repositories.Stylist
{
    public class StylistRepository : IStylistRepository
    {
        private readonly AppDbContext _context = new();

        private readonly IMapper _mapper;

        public StylistRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SlotResponseModel>> GetEmptySlotByStylistId(int? stylistId, int? salonId, int? serviceId, DateTime? date)
        {
            //var stylist = await _context.Stylists.SingleOrDefaultAsync(s=>s.Id == stylistId);
            //if(stylist == null)
            //    throw new Exception("Stylist is not found");
            // Lấy danh sách các ca làm việc (shifts) của stylist trong ngày đã chọn
            // Get all shifts for the specific stylist and date
            // Fetch stylist shifts for the given stylist and date
            // Fetch stylist shifts for the given stylist and date
            var stylistShifts = await _context.StylistShifts
                .Where(x => x.StylistId == stylistId && x.Shift.Date == date)
                .Select(x => x.Shift)
                .ToListAsync();

            if (stylistShifts == null || !stylistShifts.Any())
                throw new Exception("Stylist has not chosen any shifts on this date");

            // Fetch all non-deleted slots from the database
            var slots = await _context.Slots
                .Where(x => !x.IsDeleted)
                .ToListAsync();

            // Fetch appointment details for the stylist and the given date
            var appDetails = await _context.AppointmentDetails
                .Where(s => s.StylistId == stylistId && s.Appointment.Date == date) // Filter by stylistId and appointment date
                .ToListAsync();

            if (appDetails == null || !appDetails.Any())
                throw new Exception("Stylist of appointment not found");

            // Initialize the list for available slots
            var availableSlots = new List<SlotResponseModel>();

            // Iterate through each slot to determine its availability
            foreach (var slot in slots)
            {
                // Check if the slot time falls within any of the stylist's shifts
                bool isWithinShift = stylistShifts.Any(shift => slot.Time.TimeOfDay >= shift.StartTime.TimeOfDay && slot.Time.TimeOfDay < shift.EndTime.TimeOfDay);

                // Get the list of slots already booked for the stylist's appointments on that day
                var bookedSlots = await _context.AppointmentSlots
                    .Where(x => appDetails.Select(ad => ad.Id).Contains(x.AppointmentDetailId) && x.SlotId == slot.Id)
                    .Select(x => x.SlotId)
                    .ToListAsync();

                // If the slot is within the shift but already booked, set status to false
                // If it's within the shift and not booked, set status to true
                // If it's outside the shift, set status to false
                var status = isWithinShift && !bookedSlots.Contains(slot.Id) ? true : false;

                availableSlots.Add(new SlotResponseModel
                {
                    Id = slot.Id,
                    Time = slot.Time,
                    Status = status // Set the correct status based on the conditions
                });
            }

            // Return the available slots
            return availableSlots;




            //var slotResponse = new List<SlotResponseModel>();
            //foreach( var app in appDetailSlot)
            //{

            //    var slot = await _context.Slots.SingleOrDefaultAsync(x=>x.Id==app.SlotId);
            //    if (slot==null)
            //        appDetail.Status = "true";
            //    else
            //        appDetail.Status = "false";
            //    var response = new SlotResponseModel()
            //    {
            //        Id = slot.Id,
            //        Time = slot.Time,
            //        Status = appDetail.Status,
            //    };
            //    slotResponse.Add(response);
            //}

            //var slots = await _context.Slots.Where(slot => appDetailSlot.Any(ads => ads.SlotId == slot.Id)).ToListAsync();




        }
    }
}
