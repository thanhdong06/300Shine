using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Repository.Repositories.Salon
{
    public class SalonMappingProfile : Profile
    {
        public SalonMappingProfile() 
        {
            CreateMap<SalonEntity, SalonResponseModel>()
                .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Services))
                .ForMember(dest => dest.Stylists, opt => opt.MapFrom(src => src.Stylists));
            CreateMap<ServiceEntity, ServiceRequestModel>();
            CreateMap<StylistEntity, StylistRequestModel>();
            CreateMap<SalonEntity, SalonChoiceDTO>();
        }
        
    }
}
