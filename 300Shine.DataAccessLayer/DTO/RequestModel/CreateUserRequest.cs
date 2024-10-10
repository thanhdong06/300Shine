using _300Shine.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.BusinessObject.DTO.Request
{
    public class CreateUserRequest
    {
        public int Phone { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; }
        public bool IsStylist { get; set; }
        public bool IsVerified { get; set; } 
        public string Status { get; set; } 
        public int SalonId { get; set; }

        // Stylist specific fields
        public decimal Commission { get; set; }
        public decimal Salary { get; set; }
        public decimal SalaryPerDay { get; set; }
    }
}
