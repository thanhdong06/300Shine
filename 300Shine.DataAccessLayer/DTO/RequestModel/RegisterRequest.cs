using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.DTO.RequestModel
{
    public class RegisterRequest
    {
        public string Phone { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }    
    }
}
