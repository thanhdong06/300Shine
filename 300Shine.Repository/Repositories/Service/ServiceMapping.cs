using _300Shine.DataAccessLayer.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.DTO.RequestModel;

namespace _300Shine.Repository.Services
{
    public class ServiceMapping : Profile
    {
        public ServiceMapping() 
        {
            
            CreateMap<ServiceStyleEntity, ServiceStyleRequestModel>();
            CreateMap<ServiceEntity, ServiceResponseModel>()
                    .ForMember(dest => dest.ServiceStyles, opt => opt.MapFrom(src => src.ServiceStyles
                    .GroupBy(ss => ss.StyleId)
                    .Select(g => g.First()).ToList()));
            CreateMap<ServiceStyleEntity, StyleResponseDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Style.Id))
                    .ForMember(dest => dest.Style, opt => opt.MapFrom(src => src.Style.Style));

            CreateMap<ServiceEntity, ServiceResponseForChooseStylistFirst>()
                    .ForMember(dest => dest.ServiceStyles, opt => opt.MapFrom(src => src.ServiceStyles));
        }
    }
}
