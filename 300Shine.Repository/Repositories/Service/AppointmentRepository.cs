using _300Shine.DataAccessLayer.DBContext;
using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Repository.Repositories.Service
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

        public async Task<AppointmentEntity> CreateAppointmentAsync(AppointmentCreateDTO request, int userId)
        {
            var appointmentEntity = _mapper.Map<AppointmentEntity>(request);

            appointmentEntity.UserId = userId;
            appointmentEntity.Status = "Pending";
            appointmentEntity.Date = DateTime.Now;

            foreach (var detail in appointmentEntity.AppointmentDetails)
            {
                var requestDetail = request.Items.FirstOrDefault(d => d.ServiceId == detail.ServiceId);

                if (requestDetail != null)
                {
                    detail.StylistId = requestDetail.StylistId;
                    detail.Status = "Pending";
                    detail.Price = await _context.Services
                        .Where(s => s.Id == detail.ServiceId)
                        .Select(s => s.Price)
                        .FirstOrDefaultAsync();

                    detail.AppointmentDetailSlots = new List<AppointmentDetailSlotEntity>();
                    foreach (var slot in requestDetail.Slots)
                    {
                        detail.AppointmentDetailSlots.Add(new AppointmentDetailSlotEntity
                        {
                            SlotId = slot.Id
                        });
                    }
                }
            }

            await _context.Appointments.AddAsync(appointmentEntity);
            await _context.SaveChangesAsync();

            return appointmentEntity;
        }
    }
}
