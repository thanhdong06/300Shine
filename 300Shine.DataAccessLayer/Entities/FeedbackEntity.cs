using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.Entities
{
    [Table("Feedback")]
    public class FeedbackEntity : BaseEntity
    {
        public int AppointmentDetailId { get; set; }
        [ForeignKey(nameof(AppointmentDetailId))]
        public AppointmentDetailEntity AppointmentDetail { get; set; }
        public string? Comment { get; set; }
        public DateTime Date {  get; set; }
    }
}
