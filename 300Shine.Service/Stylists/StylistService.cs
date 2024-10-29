using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.Repository.Repositories.Salon;
using _300Shine.Repository.Repositories.Stylist;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Service.Stylists
{
    public class StylistService : IStylistService
    {
        private readonly IStylistRepository _service;
        private readonly IMapper _mapper;

        public StylistService(IStylistRepository service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<List<SlotResponseModel>> GetEmptySlotByStylistId(int? stylistId, int? salonId, int? serviceId, DateTime date)
        {
            return await _service.GetEmptySlotByStylistId(stylistId, salonId, serviceId, date);
        }

        public async Task<List<StylistResponseModel>> GetStylistBySalonAndServiceID(int salonId, int serviceId)
        {
            return await _service.GetStylistBySalonAndServiceID(salonId,serviceId);
        }
    }
}
