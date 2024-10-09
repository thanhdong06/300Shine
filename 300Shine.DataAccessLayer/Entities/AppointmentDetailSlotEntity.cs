using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.Entities
{
    [Table("AppointmentSlot")]
    public class AppointmentDetailSlotEntity : BaseEntity
    {
        public int AppointmentDetailId { get; set; }
        [ForeignKey(nameof(AppointmentDetailId))]
        public AppointmentDetailEntity AppointmentDetail { get; set; }
        public int SlotId { get; set; }
        [ForeignKey(nameof(SlotId))]
        public SlotEntity Slot { get; set; }
        
        
    }
}
