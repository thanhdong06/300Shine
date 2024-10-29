using _300Shine.DataAccessLayer.DBContext;
using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.Entities;
using AutoMapper;
using DataAccessLayer.ServiceForCRUD.Paging;
using Microsoft.EntityFrameworkCore;

namespace _300Shine.Repository.Repositories.Shift
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public ShiftRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> CreateShift(ShiftCreateDTO shift)
        {
            var existedSalon = await _context.Salons.SingleOrDefaultAsync(x => x.Id == shift.SalonId && !x.IsDeleted);
            if (existedSalon == null)
                throw new InvalidDataException("Salon is not found");
            var newShift = new ShiftEntity()
            {
                Name = shift.Name,
                Date = shift.Date,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
                MaxStaff = shift.MaxStaff,
                MinStaff = shift.MinStaff,
                Status = shift.Status,
                SalonId = shift.SalonId,
                Salon = existedSalon
            };
            _context.Shifts.Add(newShift);
            try
            {
                await _context.SaveChangesAsync();
                return "Create Shift Successfully";
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error saving changes", ex);
            }
        }

        public async Task<string> UpdateShift(ShiftUpdateDTO shift)
        {
            var existedShift = await _context.Shifts.SingleOrDefaultAsync(x => x.Id == shift.Id && !x.IsDeleted);
            if (existedShift == null)
                throw new InvalidDataException("Shift is not found");
            var existedSalon = await _context.Salons.SingleOrDefaultAsync(x => x.Id == shift.SalonId && !x.IsDeleted);
            if (existedSalon == null)
                throw new InvalidDataException("Salon is not found");
            existedShift.Name = shift.Name;
            existedShift.Date = shift.Date;
            existedShift.StartTime = shift.StartTime;
            existedShift.EndTime = shift.EndTime;
            existedShift.MinStaff = shift.MinStaff;
            existedShift.Status = shift.Status;
            existedShift.MaxStaff = shift.MaxStaff;
            existedShift.SalonId = shift.SalonId;
            existedShift.Salon = existedSalon;
            _context.Shifts.Update(existedShift);
            try
            {
                await _context.SaveChangesAsync();
                return "Update Shift Successfully";
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error saving changes", ex);
            }
        }

        public async Task<string> DeleteShift(int id)
        {
            var existedShift = await _context.Shifts.SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (existedShift == null)
                throw new InvalidDataException("Shift is not found");
            existedShift.IsDeleted = true;
            _context.Shifts.Update(existedShift);
            try
            {
                await _context.SaveChangesAsync();
                return "Delete Shift Successfully";
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error saving changes", ex);
            }
        }

        public async Task<ShiftResponseDTO> GetShiftById(int id)
        {
            var existedShift = await _context.Shifts.SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (existedShift == null)
                throw new InvalidDataException("Shift is not found");
            return _mapper.Map<ShiftResponseDTO>(existedShift);
        }

        public async Task<List<ShiftResponseDTO>> GetShifts(string? search, DateTime? date, string? status, int pageIndex, int pageSize)
        {
            var query = _context.Shifts.Where(x => x.IsDeleted == false);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(s => s.Name.Contains(search));
            if (date.HasValue)
                query = query.Where(s => s.Date == date.Value);
            if (!string.IsNullOrEmpty(status))
                query = query.Where(s => s.Status.Equals(status));

            var paginatedShifts = PaginatedList<ShiftEntity>.Create(query, pageIndex, pageSize);
            return _mapper.Map<List<ShiftResponseDTO>>(paginatedShifts);
        }
    }

}
