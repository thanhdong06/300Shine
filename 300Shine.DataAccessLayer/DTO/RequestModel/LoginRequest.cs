using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.BusinessObject.DTO.Request
{
    public class LoginRequest
    {
        [Required]
        public int Phone { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
