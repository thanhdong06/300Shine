using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.Repository.Repositories.Service;
using AutoMapper;
using DataAccessLayer.ServiceForCRUD.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Service.Services
{
    public class ServiceEntityService : IServiceEntityService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        public ServiceEntityService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }
        public async Task<string> CreateService(CreateServiceRequestModel s)
        {
            return await _serviceRepository.CreateService(s);
        }

        public async Task<string> DeleteService(int id)
        {
            return await _serviceRepository.DeleteService(id);
        }

        public async Task<ServiceResponseModel> GetServiceByID(int id)
        {
            return await _serviceRepository.GetServiceByID(id);
        }

        public async Task<List<ServiceResponseModel>> GetServices(string? search, string? sortBy, decimal? fromPrice, decimal? toPrice, string? size, int pageIndex, int pageSize)
        {
            return await _serviceRepository.GetServices(search, sortBy, fromPrice, toPrice, size, pageIndex, pageSize);
        }

        public async Task<string> UpdateService(UpdateServiceRequestModel s)
        {
            return await _serviceRepository.UpdateService(s);
        }

        public async Task<PaginatedList<ServiceResponseForChooseStylistFirst>> GetServicesByStylist(int stylistId,
            int pageIndex, int pageSize)
        {
            return await _serviceRepository.GetServicesByStylist(stylistId, pageIndex, pageSize);
        }
    }
}
