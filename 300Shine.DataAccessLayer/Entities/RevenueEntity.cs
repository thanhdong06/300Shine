using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.Entities
{
    [Table("Revenue")]
    public class RevenueEntity : BaseEntity
    {
        public decimal Amount { get; set; }
        public DateTime Date {  get; set; } 
        public int SalonId { get; set; }
        [ForeignKey(nameof(SalonId))]
        public SalonEntity Salon { get; set; }
    }
}
