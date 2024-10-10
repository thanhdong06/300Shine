using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.Entities
{
    [Table("User")]
    public class UserEntity : BaseEntity
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public int Phone { get; set; }
        public string Password { get; set; }

        public string Address { get; set; }
        public bool IsVerified { get; set; }
        public string Status { get; set; }
        public int SalonId { get; set; }
        [ForeignKey(nameof(SalonId))]
        public SalonEntity Salon { get; set; }
        public int RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public RoleEntity Role { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<AppointmentEntity> Appointments { get; set; }

    }
}
