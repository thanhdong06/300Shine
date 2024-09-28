using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.Entities
{
    [Table("StylistShift")]
    public class StylistShiftEntity : BaseEntity
    {
        public int ShiftId { get; set; }
        public int StylistId { get; set; }
        public ShiftEntity Shift { get; set; }
        public StylistEntity Stylist { get; set; }
    }
}
