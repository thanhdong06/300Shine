using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.Repository.Repositories.Salon;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Service.Salons
{
    public class SalonService : ISalonService
    {
        private readonly ISalonRepository _service;
        private readonly IMapper _mapper;

        public SalonService(ISalonRepository service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<string> CreateSalon(SalonCreateDTO s)
        {
            return await _service.CreateSalon(s);
        }

        public async Task<string> DeleteSalon(int id)
        {
            return await _service.DeleteSalon(id);
        }

        public async Task<SalonResponseModel> GetSalonByID(int id)
        {
            return await _service.GetSalonByID(id);   
        }

        public async Task<List<SalonResponseModel>> GetSalons(string? search, string? sortBy, decimal? fromPrice, decimal? toPrice, string? size, int pageIndex, int pageSize)
        {
            return await _service.GetSalons(search, sortBy, fromPrice, toPrice, size, pageIndex, pageSize);
        }

        public async Task<List<SalonChoiceDTO>> GetSalonsForChoosing(string? search, string? sortBy, string? district, string? address, int pageIndex, int pageSize)
        {
            return await _service.GetSalonsForChoosing(search, sortBy, district, address, pageIndex, pageSize);
        }

       

        public async Task<string> UpdateSalon(SalonUpdateDTO s)
        {
            return await _service.UpdateSalon(s);
        }
    }
}
