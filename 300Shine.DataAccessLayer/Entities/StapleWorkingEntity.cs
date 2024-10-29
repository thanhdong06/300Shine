using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.Entities
{
    [Table("StapleWorking")]

    public class StapleWorkingEntity : BaseEntity
    {
        public int StylistId { get; set; }
        [ForeignKey(nameof(StylistId))]
        public StylistEntity Stylist { get; set; }
        public decimal SalaryPerHour { get; set; }
        public decimal SalaryFromWorkingHour { get; set; }
        public decimal SalaryFromService {  get; set; }
        public decimal Amount { get; set; }
        public int Payday {  get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Status { get; set; }
    }
}
