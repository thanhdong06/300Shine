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

        public async Task<SalonResponseModel> GetSalonByID(int id)
        {
            return await _service.GetSalonByID(id);   
        }

        public async Task<List<SalonResponseModel>> GetSalons(string? search, string? sortBy, decimal? fromPrice, decimal? toPrice, string? size, int pageIndex, int pageSize)
        {
            return await _service.GetSalons(search, sortBy, fromPrice, toPrice, size, pageIndex, pageSize);
        }

        public async Task<List<StylistResponseModel>> GetStylistBySalonAndServiceID(int salonId, int serviceId)
        {
            return await _service.GetStylistBySalonAndServiceID(salonId, serviceId);
        }
    }
}
