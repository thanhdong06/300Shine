using _300Shine.BusinessObject.DTO.Request;
using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.DataAccessLayer.ServiceForCRUD.Mapping
{
    public class AutoMapper : Profile
    {
        public AutoMapper() {
            UserMappingProfile();
            AppointmentProfile();
            ShiftProfile();
        }
        private void UserMappingProfile()
        {
            CreateMap<UserEntity, RegisterRequest>().ReverseMap();
            CreateMap<UserEntity, LoginRequest>().ReverseMap();
            CreateMap<UserEntity, CreateUserRequest>().ReverseMap();
            CreateMap<UserEntity, UpdateUserRequest>().ReverseMap();


            CreateMap<UserEntity, ResponseUser>().ReverseMap();
        }

        private void AppointmentProfile()
        {
            CreateMap<AppointmentCreateDTO, AppointmentEntity>()
           .ForMember(dest => dest.AppointmentDetails, opt => opt.MapFrom(src => src.Items));

            CreateMap<AppointmentDetailCreateDTO, AppointmentDetailEntity>()
                .ForMember(dest => dest.AppointmentDetailSlots, opt => opt.MapFrom(src => src.Slots));

            CreateMap<SlotDTO, AppointmentDetailSlotEntity>();
        }

        private void ShiftProfile()
        {
            CreateMap<ShiftEntity, ShiftResponseDTO>().ReverseMap();
        }
    }
}
