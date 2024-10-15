using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.DTO.RequestModel
{
    public class StylistRequestModel
    {
        public int Id { get; set; }
        public decimal Commission { get; set; }
        public decimal Salary { get; set; }
        public decimal SalaryPerDay { get; set; }
       
    }
}
