using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.Entities;
using _300Shine.Repository.Repositories.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Service.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;           
        }

        public async Task<AppointmentEntity> CreateAppointmentAsync(AppointmentCreateDTO request, int userId)
        {
            return await _appointmentRepository.CreateAppointmentAsync(request, userId);
        }
    }
}
