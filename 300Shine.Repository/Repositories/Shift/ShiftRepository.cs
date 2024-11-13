using _300Shine.DataAccessLayer.DBContext;
using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.Entities;
using _300Shine.Repository.Repositories.Salon;
using AutoMapper;
using DataAccessLayer.ServiceForCRUD.Paging;
using Microsoft.EntityFrameworkCore;

namespace _300Shine.Repository.Repositories.Shift
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        private readonly ISalonRepository _salonRepository;
        public ShiftRepository(AppDbContext context, IMapper mapper, ISalonRepository salonRepository)
        {
            _context = context;
            _mapper = mapper;
            _salonRepository = salonRepository;
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

        public async Task AutoCreateShiftForWholeWeek()
        {
            var salons = await _salonRepository.GetAllSalonsAsync();
            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(6); 

            foreach (var salon in salons)
            {
                var shiftsExist = await CheckShiftsForWeek(salon.Id, startOfWeek, endOfWeek);

                if (!shiftsExist)
                {
                    int totalStylists = salon.Stylists != null ? salon.Stylists.Count : 0;
                    int minStaff = (int)Math.Ceiling(totalStylists * 0.5);
                    int maxStaff = totalStylists;

                    for (int day = 0; day < 7; day++)
                    {
                        DateTime shiftDate = startOfWeek.AddDays(day);

                        await CreateShift(salon.Id, "Morning", shiftDate, new TimeSpan(8, 0, 0), new TimeSpan(11, 30, 0), minStaff, maxStaff);
                        await CreateShift(salon.Id, "Afternoon", shiftDate, new TimeSpan(13, 0, 0), new TimeSpan(16, 30, 0), minStaff, maxStaff);
                        await CreateShift(salon.Id, "Night", shiftDate, new TimeSpan(17, 0, 0), new TimeSpan(20, 30, 0), minStaff, maxStaff);
                    }
                }
            }
        }


        private async Task CreateShift(int salonId, string name, DateTime date, TimeSpan startTime, TimeSpan endTime, int minStaff, int maxStaff)
        {
            var shift = new ShiftEntity
            {
                Name = name,
                Date = date,
                StartTime = date.Add(startTime),
                EndTime = date.Add(endTime),
                MinStaff = minStaff,
                MaxStaff = maxStaff,
                Status = "Available",
                SalonId = salonId
            };

            await _context.Shifts.AddAsync(shift);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> CheckShiftsForWeek(int salonId, DateTime startOfWeek, DateTime endOfWeek)
        {
            return await _context.Shifts
                .AnyAsync(s => s.SalonId == salonId && s.Date >= startOfWeek && s.Date <= endOfWeek);
        }

    }

}
