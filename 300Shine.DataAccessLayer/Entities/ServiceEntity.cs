using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.Entities
{
    [Table("Service")]
    public class ServiceEntity : BaseEntity
    {
        public int SalonId { get; set; }
        [ForeignKey(nameof(SalonId))]
        public SalonEntity Salon { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public ICollection<AppointmentEntity> Appointments { get; set; }
        public ICollection<ServiceStyleEntity> ServiceStyles { get; set; }
        
    }
}
