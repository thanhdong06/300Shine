using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.Entities
{
    [Table("Shift")]
    public class ShiftEntity : BaseEntity
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int MinStaff { get; set; }
        public int MaxStaff { get; set; }
        public string Status { get; set; }
        public int SalonId { get; set; }
        public SalonEntity Salon { get; set; }
    }
}
