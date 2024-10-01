using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.Entities
{
    [Table("Style")]
    public class StyleEntity : BaseEntity
    {
        public string Style { get; set; }
        public ICollection<ServiceStyleEntity> ServiceStyles { get; set; }
        public ICollection<StylistStyleEntity> StylistStyles { get; set; }
    }
}
