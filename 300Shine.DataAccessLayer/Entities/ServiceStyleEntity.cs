using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.Entities
{
    [Table("ServiceStyle")]
    public class ServiceStyleEntity : BaseEntity
    {
        public int ServiceId { get; set; }
        [ForeignKey(nameof(ServiceId))]
        public ServiceEntity Service { get; set; }
        public int StyleId { get; set; }
        [ForeignKey(nameof(StyleId))]
        public StyleEntity Style  { get; set; }
    }
}
