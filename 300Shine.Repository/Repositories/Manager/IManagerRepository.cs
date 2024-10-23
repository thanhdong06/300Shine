using _300Shine.DataAccessLayer.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Repository.Repositories.Manager
{
    public interface IManagerRepository
    {
        Task<List<StylistResponseModel>> GetAvailableStylistsAsync(int salonId, int serviceId, DateTime date);
    }
}
