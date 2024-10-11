using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.DTO.ResponseModel
{
    public class ResponseUser
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public bool IsVerified { get; set; }
        public string Status { get; set; }
        public int SalonId { get; set; }
        public string RoleName { get; set; }
        public string? ImageUrl { get; set; }

        // Stylist specific fields
        public decimal? Commission { get; set; }
        public decimal? Salary { get; set; }
        public decimal? SalaryPerDay { get; set; }
    }
}
