using _300Shine.DataAccessLayer.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Service.Manager
{
    public interface IManagerService
    {
        Task<List<StylistResponseModel>> GetAvailableStylistsAsync(int salonId, int serviceId, DateTime date);
    }
}
