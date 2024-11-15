using _300Shine.DataAccessLayer.DBContext;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.Entities;
using AutoMapper;
using DataAccessLayer.ServiceForCRUD.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

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
        public class StylistComparer : IEqualityComparer<StylistEntity>
        {
            public bool Equals(StylistEntity x, StylistEntity y)
            {
                return x.Id == y.Id;
            }



            public int GetHashCode(StylistEntity obj)
            {
                return obj.Id.GetHashCode();
            }


        }
        public async Task<List<StylistResponseModel>> GetStylistBySalonAndServiceID(int salonId, int serviceId)
        {
            var salon = await _context.Salons.Include(ss => ss.Services).Include(s => s.Stylists).AnyAsync(x => x.Id == salonId && x.IsDeleted == false);
            if (!salon)
                throw new Exception("Salon is not found");
            var serviceStyle = await _context.ServiceStyles.FirstOrDefaultAsync(x => x.ServiceId == serviceId && x.IsDeleted == false);
            if (serviceStyle == null)
                throw new Exception("Style of service is not found");

            var stylistStyle = await _context.StylistStyles.Where(x => x.StyleId == serviceStyle.StyleId).ToListAsync();
            if (stylistStyle == null)
                throw new Exception("Stylist of style is not found");

            var stylistList = new List<StylistResponseModel>();

            foreach (var stylist in stylistStyle)
            {
                var stylists = await _context.Stylists.Include(u => u.User).FirstOrDefaultAsync(x => x.Id == stylist.StylistId && x.SalonId == salonId);
                var stylistResponse = new StylistResponseModel()
                {
                    Id = stylists.Id,
                    Name = stylists.User.FullName,
                    ImageUrl = stylists.User.ImageUrl,
                };
                stylistList.Add(stylistResponse);

            }
            return stylistList;

        }
        public async Task<List<SlotResponseModel>> GetEmptySlotByStylistId(int? stylistId , int? salonId, int? serviceId, DateTime date)
        {
            if (stylistId != 0)
            {
                var stylistShifts = await _context.StylistShifts
     .Where(x => x.StylistId == stylistId  && x.Shift.Date.Year == date.Year && x.Shift.Date.Month == date.Month && x.Shift.Date.Day == date.Day) //sửa so sánh ngày 
     .Select(x => x.Shift)
     .ToListAsync();

                if (stylistShifts == null || !stylistShifts.Any())
                    throw new Exception("Stylist has not chosen any shifts on this date");

                // Fetch all non-deleted slots from the database
                var slots = await _context.Slots
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();
                var service = await ( from svcStyle in _context.ServiceStyles
                               join ss in _context.StylistStyles on svcStyle.StyleId equals ss.StyleId
                               join stylist in _context.Stylists on ss.StylistId equals stylist.Id
                               where svcStyle.ServiceId == serviceId
                               select stylist).Distinct().ToListAsync();
                if (service == null || !service.Any())
                    throw new Exception("Stylist of service not found");
                // Fetch appointment details for the stylist and the given date
                var appDetails = await _context.AppointmentDetails
                    .Where(s => s.StylistId == stylistId && s.Appointment.Date.Year == date.Year && s.Appointment.Date.Month == date.Month && s.Appointment.Date.Day == date.Day && s.Appointment.Status != "Canceled") // Filter by stylistId and appointment date, sửa so sánh ngày giờ  
                    .ToListAsync();
                //if (appDetails == null || !appDetails.Any())
                //    throw new Exception("Stylist of appoinment not found");


                // Initialize the list for available slots
                var availableSlots = new List<SlotResponseModel>();

                // Iterate through each slot to determine its availability
                foreach (var slot in slots)
                {
                    // Check if the slot time falls within any of the stylist's shifts
                    bool isWithinShift = stylistShifts.Any(shift => slot.Time.TimeOfDay >= shift.StartTime.TimeOfDay && slot.Time.TimeOfDay <=shift.EndTime.TimeOfDay);

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
                    
                }return availableSlots;
            }
            else
            {
                var stylistInSalon = await _context.Stylists.Where(s => s.SalonId == salonId).ToListAsync();
                if (stylistInSalon == null)
                    throw new Exception("Salon of stylist not found");

                var styleService = await _context.ServiceStyles.FirstOrDefaultAsync(s => s.Service.Id == serviceId);

                var stylistStyle = await _context.StylistStyles.Include( s => s.Stylist).Where(s => s.StyleId == styleService.StyleId).Select(s => s.Stylist).ToListAsync();


                var commonStylists = stylistInSalon.Intersect(stylistStyle, new StylistComparer()).ToList();

                if (!commonStylists.Any())
                {
                    throw new Exception("Salon do not cotain any stylist for this service");
                }
                else
                {
                    var availableSlots = new List<SlotResponseModel>();
                    foreach (var stylist in commonStylists)
                    {
                        var stylistShifts = await _context.StylistShifts
    .Where(x => x.StylistId == stylist.Id && x.Shift.Date.Year == date.Year && x.Shift.Date.Month == date.Month && x.Shift.Date.Day == date.Day ) //sửa so sánh ngày 
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
                            .Where(s => s.StylistId == stylist.Id && s.Appointment.Date.Year == date.Year && s.Appointment.Date.Month == date.Month && s.Appointment.Date.Day == date.Day && s.Appointment.Status != "Canceled") // Filter by stylistId and appointment date, sửa so sánh ngày giờ  
                            .ToListAsync();

                        if (appDetails == null || !appDetails.Any())
                            throw new Exception("Stylist of appointment not found");

                        // Initialize the list for available slots
                        

                        // Iterate through each slot to determine its availability
                        foreach (var slot in slots)
                        {
                            // Check if the slot time falls within any of the stylist's shifts
                            bool isWithinShift = stylistShifts.Any(shift => slot.Time.TimeOfDay >= shift.StartTime.TimeOfDay && slot.Time.TimeOfDay <= shift.EndTime.TimeOfDay);

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
                    }
                    // Return the available slots
                    return availableSlots;
                }

            }
        }
        public async Task<List<StylistResponseModel>> GetStylistsBySalon(int salonId, string? search, int pageIndex, int pageSize)
        {
            var stylistList = _context.Stylists.Include(x => x.User).Where(x => x.SalonId == salonId && !x.IsDeleted);
            if (!string.IsNullOrEmpty(search))
            {
                stylistList = stylistList.Where(x => x.User.FullName.ToLower().Contains(search.ToLower()));
            }
            var paginatedStylists = PaginatedList<StylistEntity>.Create(stylistList, pageIndex, pageSize);
            return _mapper.Map<List<StylistResponseModel>>(paginatedStylists);
        }

        public async Task<List<StylistResponseModel>> GetAllStylist(int pageIndex, int pageSize)
        {
            var stylist = _context.Stylists.Include(x => x.User).Where(x=> !x.IsDeleted);
            if (stylist == null)
            {
                throw new Exception("There is no stylist to reveal");
            }
            var paginatedStylists = PaginatedList<StylistEntity>.Create(stylist, pageIndex, pageSize);
            return _mapper.Map<List<StylistResponseModel>>(paginatedStylists);
        }

        public async Task<StylistResponseModel> GetStylistById(int stylistId)
        {
            var stylist = await _context.Stylists.Include(x => x.User).SingleOrDefaultAsync(x => x.Id == stylistId && !x.IsDeleted);
            if (stylist == null)
            {
                throw new Exception("Not found this stylist");
            }
            return _mapper.Map<StylistResponseModel>(stylist);
        }
    }
}
