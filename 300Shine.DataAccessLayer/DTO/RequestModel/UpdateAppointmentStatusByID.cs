using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.DTO.RequestModel
{
    public class UpdateAppointmentStatusByID
    {
        public int AppointmentDetailId { get; set; }
        public string Status { get; set; }
    }
}
