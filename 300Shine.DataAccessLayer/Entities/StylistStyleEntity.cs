using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.Entities
{
    [Table("StylistStyle")]
    public class StylistStyleEntity : BaseEntity
    {
        public int StylistId { get; set; }
        [ForeignKey(nameof(StylistId))]
        public StylistEntity Stylist { get; set; }
        public int StyleId { get; set; }
        [ForeignKey(nameof(StyleId))]
        public StyleEntity Style { get; set; }
    }
}
