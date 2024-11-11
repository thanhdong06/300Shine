using _300Shine.DataAccessLayer.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Service.Stylists
{
    public interface IStylistService
    {
        Task<List<SlotResponseModel>> GetEmptySlotByStylistId(int? stylistId, int? salonId, int? serviceId, DateTime date);
        Task<List<StylistResponseModel>> GetStylistBySalonAndServiceID(int salonId, int serviceId);
        Task<List<StylistResponseModel>> GetStylistsBySalon(int salonId, string? search, int pageIndex, int pageSize);
        Task<List<StylistResponseModel>> GetAllStylist(int pageIndex, int pageSize);
    }
}
