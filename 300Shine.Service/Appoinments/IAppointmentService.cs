using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Service.Appoinments
{
    public interface IAppointmentService
    {
        Task<AppointmentEntity> CreateAppointmentAsync(AppointmentCreateDTO request, int userId, int OrderCode);
        Task<string> CreateAppointmentDetailWithReturnDayAsync(AppointmentDetailCreateWithReturnDateRequest request);
    }
}
