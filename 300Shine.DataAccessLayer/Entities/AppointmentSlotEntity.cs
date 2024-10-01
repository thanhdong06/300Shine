using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.Entities
{
    [Table("AppointmentSlot")]
    public class AppointmentSlotEntity : BaseEntity
    {
        public int AppointmentId { get; set; }
        [ForeignKey(nameof(AppointmentId))]
        public AppointmentEntity Appointment { get; set; }
        public int SlotId { get; set; }
        [ForeignKey(nameof(SlotId))]
        public SlotEntity Slot { get; set; }
        
        
    }
}
