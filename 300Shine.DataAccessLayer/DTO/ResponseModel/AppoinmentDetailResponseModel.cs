using _300Shine.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.DTO.ResponseModel
{
    public class AppoinmentDetailResponseModel
    {
        public int AppointmentId { get; set; }
        public string ServiceName { get; set; }
        public string StylistName { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }

        public ICollection<AppointmentDetailSlotResponse> AppointmentDetailSlots { get; set; }
    }
}
