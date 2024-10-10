using _300Shine.DataAccessLayer.DBContext;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.Entities;
using AutoMapper;
using DataAccessLayer.ServiceForCRUD.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Repository.Repositories.Salon
{
    public class SalonRepository : ISalonRepository
    {
        private readonly AppDbContext _context = new();

        private readonly IMapper _mapper;

        public SalonRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<SalonResponseModel>> GetSalons(string? search, string? sortBy, decimal? fromPrice, decimal? toPrice, string? size, int pageIndex, int pageSize)
        {
            IQueryable<SalonEntity> salons = _context.Salons.Include(ss=>ss.Services).Include(s=>s.Stylists).Where(x => x.Id != null && x.IsDeleted == false);

            var paginatedUsers = PaginatedList<SalonEntity>.Create(salons, pageIndex, pageSize);

            return _mapper.Map<List<SalonResponseModel>>(paginatedUsers);  
        }

        public async Task<SalonResponseModel> GetSalonByID(int id)
        {
            var salon = await _context.Salons.Include(ss => ss.Services).Include(s=>s.Stylists).SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (salon == null || salon.IsDeleted == true)
                throw new Exception("Service is not found");

            return _mapper.Map<SalonResponseModel>(salon);
        }
    }
}
