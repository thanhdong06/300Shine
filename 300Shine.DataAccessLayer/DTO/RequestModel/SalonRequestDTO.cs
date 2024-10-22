using _300Shine.DataAccessLayer.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.DTO.RequestModel
{
    public class SalonRequestDTO
    {
        public int Id { get; set; } 
        public string Address { get; set; }
        public int Phone { get; set; }
        public string District { get; set; }
        public List<ServiceRequestModel> Services { get; set; }
        public List<StylistRequestModel> Stylists { get; set; }  
    }
    public class SalonCreateDTO
    {
        public string ImageUrl { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string District { get; set; }
    }
    public class SalonUpdateDTO
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string District { get; set; }
    }

    public class SalonChoiceDTO
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string District { get; set; }
    }
    public class SalonStylistRequestDTO
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public List<StylistRequestModel> Stylists { get; set; }

    }
}
