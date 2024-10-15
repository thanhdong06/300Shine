using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.DTO.RequestModel
{
    public class UpdateUserRequest
    {
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; } 
        public bool? Gender { get; set; }           
        public string? Phone { get; set; }            
        public string Address { get; set; }
        public int? RoleId { get; set; }          
        public bool? IsStylist { get; set; }       
        public bool? IsVerified { get; set; }      
        public string Status { get; set; }
        public int? SalonId { get; set; }           

        // Stylist specific fields
        public decimal? Commission { get; set; }   
        public decimal? Salary { get; set; }        
        public decimal? SalaryPerDay { get; set; }  
    }
}
