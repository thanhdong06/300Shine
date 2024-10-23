using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.Repository.Repositories.Manager;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Service.Manager
{
    public class ManagerService : IManagerService
    {
        private readonly IConfiguration _configuration;
        private readonly IManagerRepository _managerRepository;

        public ManagerService(IConfiguration configuration, IManagerRepository managerRepository)
        {
            _configuration = configuration;
            _managerRepository = managerRepository;
        }

        public async Task<List<StylistResponseModel>> GetAvailableStylistsAsync(int salonId, int serviceId, DateTime date)
        {
            return await _managerRepository.GetAvailableStylistsAsync(salonId, serviceId, date);
        }
    }
}
