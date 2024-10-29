
using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.Entities;
using DataAccessLayer.ServiceForCRUD.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Repository.Repositories.Service
{
    public interface IServiceRepository
    {
        Task<string> CreateService(CreateServiceRequestModel s);

        Task<string> UpdateService(UpdateServiceRequestModel s);

        Task<string> DeleteService(int id);

        Task<List<ServiceResponseModel>> GetServices(string? search, string? sortBy,
        decimal? fromPrice, decimal? toPrice,
            string? size,
            int pageIndex, int pageSize);

        Task<ServiceResponseModel> GetServiceByID(int id);

        Task<PaginatedList<ServiceResponseForChooseStylistFirst>> GetServicesByStylist(int stylistId,
            int pageIndex, int pageSize);
    }
}
