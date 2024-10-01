using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.Entities
{
    [Table("Stylist")]
    public class StylistEntity : BaseEntity
    {
        public decimal Commission { get; set; }
        public decimal Salary { get; set; }
        public decimal SalaryPerDay { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public UserEntity User { get; set; }
        public int SalonId { get; set; }
        [ForeignKey(nameof(SalonId))]
        public SalonEntity Salon { get; set; }

        public ICollection<AppointmentDetailEntity> AppointmentDetails { get; set; }
        public ICollection<StylistStyleEntity> StylistStyles { get; set; }
        public ICollection<StylistShiftEntity> StylistShifts { get; set; }
        public ICollection<StapleWorkingEntity> StapleWorkings { get; set; }

    }
}
