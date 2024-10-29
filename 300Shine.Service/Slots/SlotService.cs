using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.Repository.Repositories.Slot;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Service.Slots
{
    public class SlotService : ISlotService
    {
        private readonly ISlotRepository _slotService;
        private IMapper _mapper;

        public SlotService(ISlotRepository slotService, IMapper mapper)
        {
            _slotService = slotService;
            _mapper = mapper;
        }

        public async Task<List<SlotResponseModel>> GetSlotByStylistIdAndDate(int stylistId, DateTime date)
        {
            return await _slotService.GetSlotByStylistIdAndDate(stylistId, date);
        }
    }
}
