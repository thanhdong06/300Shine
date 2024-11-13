using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.Entities;
using _300Shine.Repository.Repositories.Appoinment;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Service.Appoinments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<AppointmentEntity> CreateAppointmentAsync(AppointmentCreateDTO request, int userId, int OrderCode)
        {
            return await _appointmentRepository.CreateAppointmentAsync(request, userId, OrderCode);
        }

        public async Task<string> CreateAppointmentDetailWithReturnDayAsync(AppointmentDetailCreateWithReturnDateRequest request)
        {
            return await _appointmentRepository.CreateAppointmentDetailWithReturnDayAsync(request);
        } 
        public async Task<AppointmentEntity> UpdateAppointmentStatusAsync(int orderCode, string status)
        {
            return await _appointmentRepository.UpdateAppointmentStatusAsync(orderCode, status);
        }

        public async Task<AppointmentEntity> UpdateAppointmentById(int appointmentId, string status)
        {
            return await _appointmentRepository.UpdateAppointmentById(appointmentId, status);
        }

    }
}
