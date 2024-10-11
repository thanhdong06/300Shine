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
                throw new Exception("Salon is not found");

            return _mapper.Map<SalonResponseModel>(salon);
        }

        public async Task<List<StylistResponseModel>> GetStylistBySalonAndServiceID(int salonId, int serviceId)
        {
            var salon = await _context.Salons.Include(ss => ss.Services).Include(s => s.Stylists).AnyAsync(x => x.Id == salonId && x.IsDeleted == false);
            if (!salon)
                throw new Exception("Salon is not found");
            var serviceStyle = await _context.ServiceStyles.SingleOrDefaultAsync(x => x.ServiceId == serviceId && x.IsDeleted == false);
            if (serviceStyle == null)
                throw new Exception("Style of service is not found");

            var stylistStyle = await _context.StylistStyles.Where(x => x.StyleId == serviceStyle.StyleId).ToListAsync();
            if(stylistStyle == null)
                throw new Exception("Stylist of style is not found");

            var stylistList = new List<StylistResponseModel>();

            foreach(var stylist in stylistStyle)
            {
                var stylists = await _context.Stylists.Include(u=>u.User).SingleOrDefaultAsync(x => x.Id == stylist.StylistId && x.SalonId==salonId);
                var stylistResponse = new StylistResponseModel() 
                {
                    Id = stylists.Id,
                    Name = stylists.User.FullName,
                    ImageUrl = stylists.User.ImageUrl,
                };
                stylistList.Add(stylistResponse);

            }
            return stylistList;

        }
    }
}
