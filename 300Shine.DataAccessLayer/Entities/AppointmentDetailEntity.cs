using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.Entities
{
    [Table("AppointmentDetail")]
    public class AppointmentDetailEntity : BaseEntity
    {
        public int AppointmentId { get; set; }
        [ForeignKey(nameof(AppointmentId))]
        public AppointmentEntity Appointment { get; set; }

        public int ServiceId { get; set; }
        [ForeignKey(nameof(ServiceId))]
        public ServiceEntity Service { get; set; }

        public int StylistId { get; set; }
        [ForeignKey(nameof(StylistId))]
        public StylistEntity Stylist { get; set; }

        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}
