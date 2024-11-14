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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace _300Shine.Repository.Repositories.Style
{
    public class StyleRepository : IStyleRepository
    {
        private readonly AppDbContext _context=null;

        private readonly IMapper _mapper;

        public StyleRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<StyleResponseDTO>> GetAllStyles(string? search, int pageIndex, int pageSize)
        {
           var styles =_context.Styles.Where(x=>x.IsDeleted==false);
            if (!string.IsNullOrEmpty(search))
                styles = styles.Where(s => s.Style.Contains(search));
            var paginatedStyles = PaginatedList<StyleEntity>.Create(styles, pageIndex, pageSize);
            return _mapper.Map<List<StyleResponseDTO>>(paginatedStyles);

        }
    }
}
