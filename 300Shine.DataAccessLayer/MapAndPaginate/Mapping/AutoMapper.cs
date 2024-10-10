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
        }
        private void UserMappingProfile()
        {
            CreateMap<UserEntity, RegisterRequest>().ReverseMap();
            CreateMap<UserEntity, LoginRequest>().ReverseMap();
            CreateMap<UserEntity, CreateUserRequest>().ReverseMap();
            CreateMap<UserEntity, UpdateUserRequest>().ReverseMap();


            CreateMap<UserEntity, ResponseUser>().ReverseMap();
        }
    }
}
