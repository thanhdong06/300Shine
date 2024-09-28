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
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public int RoleId { get; set; }

        public RoleEntity Role { get; set; }

    }
}
