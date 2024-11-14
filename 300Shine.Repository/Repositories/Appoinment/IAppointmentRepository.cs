using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Repository.Repositories.Appoinment
{
    public interface IAppointmentRepository
    {
        Task<AppointmentEntity> CreateAppointmentAsync(AppointmentCreateDTO request, int userId, int OrderCode);
        Task<string> CreateAppointmentDetailWithReturnDayAsync(AppointmentDetailCreateWithReturnDateRequest request);
        Task<AppointmentEntity> UpdateAppointmentStatusAsync(int orderCode, string status);
        Task<AppointmentDetailEntity> UpdateAppointmentById(int appointmentId, string status);
        Task<List<AppointmentResponseModel>> GetAppoinmentByUserId(int userId, string status, string appoinmentDetailStatus);
        Task<List<AppointmentResponseModel>> GetAppoinmentByStylistId(int stylistId, string status, string appoinmentDetailStatus);
        Task<List<AppointmentResponseModel>> GetAppoinmentsByStatus(string status);
    }
}
