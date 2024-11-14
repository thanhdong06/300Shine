using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.Repository.Repositories.Slot;
using _300Shine.Repository.Repositories.Style;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Service.Styles
{

    public class StyleService : IStyleService
    {
        private readonly IStyleRepository _styleService;
        private IMapper _mapper;

        public StyleService(IStyleRepository styleService, IMapper mapper)
        {
            _styleService = styleService;
            _mapper = mapper;
        }

        public async Task<List<StyleResponseDTO>> GetAllStyles(string? search, int pageIndex, int pageSize)
        {
            return await _styleService.GetAllStyles(search, pageIndex, pageSize);
        }
    }
}
