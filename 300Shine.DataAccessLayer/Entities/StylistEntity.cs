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
        public int SalonId { get; set; }
        public SalonEntity Salon { get; set; }
        public UserEntity User { get; set; }
    }
}
