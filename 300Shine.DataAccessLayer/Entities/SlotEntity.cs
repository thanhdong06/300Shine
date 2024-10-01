using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.Entities
{
    [Table("Slot")]
    public class SlotEntity : BaseEntity
    {
        public DateTime Time { get; set; }
        public ICollection<AppointmentSlotEntity> AppointmentSlots { get; set; }
    }
}
