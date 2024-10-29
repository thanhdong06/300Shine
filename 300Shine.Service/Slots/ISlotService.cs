using _300Shine.DataAccessLayer.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Service.Slots
{
    public interface ISlotService
    {
        Task<List<SlotResponseModel>> GetSlotByStylistIdAndDate(int stylistId, DateTime date);
    }
}
