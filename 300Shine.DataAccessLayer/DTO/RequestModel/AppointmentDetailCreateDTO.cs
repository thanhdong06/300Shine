using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.DTO.RequestModel
{
    public class AppointmentDetailCreateDTO
    {
        public int ServiceId { get; set; }
        public int StylistId { get; set; }
        public List<SlotDTO> Slots { get; set; }
    }
}
