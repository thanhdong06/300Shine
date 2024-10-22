using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Repository.Repositories.Salon
{
    public interface ISalonRepository
    {
        Task<string> CreateSalon(SalonCreateDTO s);

        Task<string> UpdateSalon(SalonUpdateDTO s);

        Task<string> DeleteSalon(int id);

        Task<List<StylistResponseModel>> GetStylistBySalonAndServiceID(int salonId, int serviceId);
        Task<List<SalonResponseModel>> GetSalons(string? search, string? sortBy,
           decimal? fromPrice, decimal? toPrice,
           string? size,
           int pageIndex, int pageSize);

        Task<List<SalonChoiceDTO>> GetSalonsForChoosing(string? search, string? sortBy,
           string? district, string? address,
           int pageIndex, int pageSize);

        Task<SalonResponseModel> GetSalonByID(int id);
    }
}
