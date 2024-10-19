using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.DTO.RequestModel
{
    public class AppointmentCreateDTO
    {
        public int SalonId { get; set; }
        public List<AppointmentDetailCreateDTO> Items { get; set; }
    }
    public class AppointmentDetailCreateWithReturnDateRequest
    {    
        public int AppointmentId { get; set; }
        public int StylistId { get; set; }
        public DateTime ReturnDate { get; set; }
        public List<SlotDTO> Slots { get; set; }
    }
}
