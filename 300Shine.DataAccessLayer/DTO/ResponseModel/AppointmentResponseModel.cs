using _300Shine.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.DTO.ResponseModel
{
    public class AppointmentResponseModel
    {
        public string AppointmentId { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public string UserName { get; set; } 
        public string SalonAddress { get; set; }     
        public int OrderCode { get; set; }
        public ICollection<AppoinmentDetailResponseModel> AppointmentDetails { get; set; }
    }
    
}
