using _300Shine.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.DTO.ResponseModel
{
    public class AppointmentDetailSlotResponse
    {
        public int SlotId { get; set; }
       
        public DateTime? Slot { get; set; }
       
    }
}
