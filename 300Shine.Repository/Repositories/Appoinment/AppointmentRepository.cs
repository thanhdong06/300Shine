using _300Shine.DataAccessLayer.DBContext;
using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Repository.Repositories.Appoinment
{
    public class AppointmentRepository : IAppointmentRepository
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AppointmentRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppointmentEntity> CreateAppointmentAsync(AppointmentCreateDTO request, int userId, int OrderCode)
        {
            var appointmentEntity = _mapper.Map<AppointmentEntity>(request);

            appointmentEntity.UserId = userId;
            appointmentEntity.Status = "Canceled";
            appointmentEntity.Date = request.DateToGo;
            appointmentEntity.OrderCode = OrderCode;

            decimal totalAmount = 0;

            foreach (var detail in appointmentEntity.AppointmentDetails)
            {
                var requestDetail = request.Items.FirstOrDefault(d => d.ServiceId == detail.ServiceId);

                if (requestDetail != null)
                {
                    detail.StylistId = requestDetail.StylistId;
                    detail.Status = "Pending";
                    var service = await _context.Services
                                .Where(s => s.Id == detail.ServiceId)
                                .Select(s => new { s.Price, s.Type })
                                .FirstOrDefaultAsync();

                    if (service != null)
                    {
                        detail.Price = service.Price;
                        totalAmount += service.Price;

                        detail.Type = service.Type;

                        if (service.Type == "onetime")
                        {
                            detail.ReturnDate = request.DateToGo.AddDays(7);
                        }
                        else
                        {
                            detail.ReturnDate = request.DateToGo;
                        }
                    }
                    detail.AppointmentDetailSlots = new List<AppointmentDetailSlotEntity>();
                    foreach (var slot in requestDetail.Slots)
                    {
                        detail.AppointmentDetailSlots.Add(new AppointmentDetailSlotEntity
                        {
                            SlotId = slot.Id,
                        });
                    }
                }
            }
            appointmentEntity.Amount = totalAmount;

            await _context.Appointments.AddAsync(appointmentEntity);
            await _context.SaveChangesAsync();

            return appointmentEntity;
        }
        public async Task<string> CreateAppointmentDetailWithReturnDayAsync(AppointmentDetailCreateWithReturnDateRequest request)
        {
            var app = await _context.Appointments.Include(x => x.AppointmentDetails).SingleOrDefaultAsync(a => a.Id == request.AppointmentId);
            if (app == null)
                throw new Exception("Appointment not found");

            var appDetail = await _context.AppointmentDetails.SingleOrDefaultAsync(a => a.AppointmentId == app.Id); ;
            if (appDetail == null)
                throw new Exception("Appointment Detail do not contain this Appoinment");
            var appointmentDetail = new AppointmentDetailEntity
            {
                AppointmentId = appDetail.AppointmentId,
                StylistId = request.StylistId,
                ReturnDate = request.ReturnDate,
                ServiceId = appDetail.ServiceId,
                Price = appDetail.Price,
                Type = appDetail.Type,
                Status = appDetail.Status,
                // ... other properties as needed
                AppointmentDetailSlots = new List<AppointmentDetailSlotEntity>()

            };

            //await _context.AppointmentDetails.AddAsync(appointmentDetail);
            foreach (var slot in request.Slots)
            {
                var slots = await _context.Slots.SingleOrDefaultAsync(x => x.Id == slot.Id);
                if (slots == null)
                {

                    throw new Exception("Slot not found.");
                }

                var appointmentDetailSlot = new AppointmentDetailSlotEntity
                {
                    AppointmentDetail = appointmentDetail,
                    Slot = slots
                };

                appointmentDetail.AppointmentDetailSlots.Add(appointmentDetailSlot);
            }

            await _context.AppointmentDetails.AddAsync(appointmentDetail);
            try
            {
                await _context.SaveChangesAsync();
                return "Create Successfully";
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error saving changes", ex);
            }
        }

        public async Task<List<AppointmentResponseModel>> GetAppoinmentByUserId(int userId, string status, string appoinmentDetailStatus)
        {
            var appointments = await _context.Appointments
           .Where(a => a.UserId == userId && a.Status.Equals(status) && a.AppointmentDetails
                            .Any(ad => ad.Status == appoinmentDetailStatus))
           .Include(a => a.User)
           .Include(a => a.Salon)
           .Include(a => a.AppointmentDetails)
           .ThenInclude(ad => ad.Service)
           .Include(a => a.AppointmentDetails)
            .ThenInclude(ad => ad.AppointmentDetailSlots)
                .ThenInclude(s => s.Slot)
            .Include(a => a.AppointmentDetails)
            .ThenInclude(ad => ad.Stylist).ThenInclude(u => u.User)

           .ToListAsync();
            // Map the AppointmentEntity to AppointmentResponseModel
            var appointmentDtos = appointments.Select(a => new AppointmentResponseModel
            {
                AppointmentId = a.Id,
                Note = a.Note,
                Date = a.Date,
                Status = a.Status,
                Amount = a.Amount,
                UserName = a.User?.FullName,
                SalonAddress = a.Salon?.Address,
                OrderCode = a.OrderCode ?? default(int),
                AppointmentDetails = a.AppointmentDetails.Select(ad => new AppoinmentDetailResponseModel
                {
                    AppointmentId = ad.AppointmentId,
                    ServiceName = ad.Service?.Name,
                    StylistName = ad.Stylist?.User?.FullName,
                    ReturnDate = ad.ReturnDate,
                    Price = ad.Price,
                    Type = ad.Type,
                    Status = ad.Status,
                    AppointmentDetailSlots = ad.AppointmentDetailSlots.Select(sl => new AppointmentDetailSlotResponse
                    {
                        SlotId = sl.SlotId,
                        Slot = sl.Slot?.Time,
                    }).ToList()
                }).ToList()

            }).ToList();
            return appointmentDtos;
        }

        public async Task<List<AppointmentResponseModel>> GetAppoinmentsByStatus(string status, string process)
        {
            var appointments = await _context.Appointments
           .Where(a => a.Status.Equals(status)  && a.AppointmentDetails
                            .Any(ad => ad.Status == process))
           .Include(a => a.User)
           .Include(a => a.Salon)
           .Include(a => a.AppointmentDetails)
           .ThenInclude(ad => ad.Service)
           .Include(a => a.AppointmentDetails)
            .ThenInclude(ad => ad.AppointmentDetailSlots)
                .ThenInclude(s => s.Slot)
            .Include(a => a.AppointmentDetails)
            .ThenInclude(ad => ad.Stylist).ThenInclude(u => u.User)

           .ToListAsync();
            // Map the AppointmentEntity to AppointmentResponseModel
            var appointmentDtos = appointments.Select(a => new AppointmentResponseModel
            {
                AppointmentId = a.Id,
                Note = a.Note,
                Date = a.Date,
                Status = a.Status,
                Amount = a.Amount,
                UserName = a.User?.FullName,
                SalonAddress = a.Salon?.Address,
                OrderCode = a.OrderCode ?? default(int),
                AppointmentDetails = a.AppointmentDetails.Select(ad => new AppoinmentDetailResponseModel
                {
                    AppointmentId = ad.AppointmentId,
                    ServiceName = ad.Service?.Name,
                    StylistName = ad.Stylist?.User?.FullName,
                    ReturnDate = ad.ReturnDate,
                    Price = ad.Price,
                    Type = ad.Type,
                    Status = ad.Status,
                    AppointmentDetailSlots = ad.AppointmentDetailSlots.Select(sl => new AppointmentDetailSlotResponse
                    {
                        SlotId = sl.SlotId,
                        Slot = sl.Slot?.Time,
                    }).ToList()
                }).ToList()

            }).ToList();
            return appointmentDtos;
        }

        public async Task<AppointmentEntity> UpdateAppointmentStatusAsync(int orderCode, string status)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.OrderCode == orderCode);

            if (appointment == null)
            {
                throw new KeyNotFoundException($"Appointment with OrderCode {orderCode} not found.");
            }

            appointment.Status = status;
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<AppointmentDetailEntity> UpdateAppointmentById(int appointmentId, string status)
        {
            var appointment = await _context.AppointmentDetails.FirstOrDefaultAsync(p => p.AppointmentId == appointmentId);

            if (appointment == null)
            {
                throw new KeyNotFoundException($"Appointment with ID {appointmentId} not found.");
            }

            appointment.Status = status;
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<List<AppointmentResponseModel>> GetAppoinmentByStylistId(int userId, string status, string appoinmentDetailStatus)
        {
            var stylist = await _context.Stylists.SingleOrDefaultAsync(x => x.UserId == userId);

            var appointments = await _context.Appointments
                .Where(a => a.Status == status &&
                            a.AppointmentDetails
                            .Any(ad => ad.StylistId == stylist.Id && ad.Status == appoinmentDetailStatus))
          .Include(a => a.User)
          .Include(a => a.Salon)
          .Include(a => a.AppointmentDetails)
          .ThenInclude(ad => ad.Stylist)
          .Include(a => a.AppointmentDetails)
          .ThenInclude(ad => ad.Service)
          .Include(a => a.AppointmentDetails)
           .ThenInclude(ad => ad.AppointmentDetailSlots)
               .ThenInclude(s => s.Slot)
           .Include(a => a.AppointmentDetails)
           .ThenInclude(ad => ad.Stylist).ThenInclude(u => u.User)
          .ToListAsync();
            // Map the AppointmentEntity to AppointmentResponseModel
            var appointmentDtos = appointments.Select(a => new AppointmentResponseModel
            {
                AppointmentId = a.Id,
                Note = a.Note,
                Date = a.Date,
                Status = a.Status,
                Amount = a.Amount,
                UserName = a.User?.FullName,
                SalonAddress = a.Salon?.Address,
                OrderCode = a.OrderCode ?? default(int),
                AppointmentDetails = a.AppointmentDetails.Select(ad => new AppoinmentDetailResponseModel
                {
                    AppointmentId = ad.AppointmentId,
                    ServiceName = ad.Service?.Name,
                    StylistName = ad.Stylist?.User?.FullName,
                    ReturnDate = ad.ReturnDate,
                    Price = ad.Price,
                    Type = ad.Type,
                    Status = ad.Status,
                    AppointmentDetailSlots = ad.AppointmentDetailSlots.Select(sl => new AppointmentDetailSlotResponse
                    {
                        SlotId = sl.SlotId,
                        Slot = sl.Slot?.Time,
                    }).ToList()
                }).ToList()

            }).ToList();
            return appointmentDtos;
        }
    }
}
