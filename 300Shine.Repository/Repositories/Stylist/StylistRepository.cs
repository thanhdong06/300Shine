using _300Shine.DataAccessLayer.DBContext;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _300Shine.Repository.Repositories.Stylist
{
    public class StylistRepository : IStylistRepository
    {
        private readonly AppDbContext _context = new();

        private readonly IMapper _mapper;

        public StylistRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SlotResponseModel>> GetEmptySlotByStylistId(int? stylistId, int? salonId, int? serviceId, DateTime? date)
        {
            //var stylist = await _context.Stylists.SingleOrDefaultAsync(s=>s.Id == stylistId);
            //if(stylist == null)
            //    throw new Exception("Stylist is not found");
            // Lấy danh sách các ca làm việc (shifts) của stylist trong ngày đã chọn
            var stylistShifts = await _context.StylistShifts    
                                              .Where(x => x.StylistId == stylistId && x.Shift.Date == date)
                                              .Select(x => x.Shift)
                                              .ToListAsync();

            if (stylistShifts == null || !stylistShifts.Any())
                throw new Exception("Stylist has not chosen any shifts on this date");

            // Lấy tất cả các slot không bị xóa từ cơ sở dữ liệu
            var slots = await _context.Slots
                                      .Where(x => !x.IsDeleted)
                                      .ToListAsync();

            // Lấy thông tin chi tiết của stylist
            var appDetail = await _context.AppointmentDetails.SingleOrDefaultAsync(s => s.StylistId == stylistId);
            if (appDetail == null)
                throw new Exception("Stylist of appointment not found");

            // Lấy danh sách các slot đã được đặt trước cho cuộc hẹn của stylist
            var appDetailSlot = await _context.AppointmentSlots
                                              .Where(x => x.AppointmentDetailId == appDetail.Id)
                                              .Select(ads => ads.SlotId)
                                              .ToListAsync();

            // Lọc các slot có sẵn trong các ca làm việc của stylist và chưa được đặt
            var availableSlots = slots.Select(s => new SlotResponseModel
            {
                Id = s.Id,
                Time = s.Time,

                // Kiểm tra xem thời gian của slot có nằm trong khoảng thời gian của ca làm việc nào của stylist không
                Status = stylistShifts.Any(shift => s.Time.TimeOfDay >= shift.StartTime.TimeOfDay && s.Time.TimeOfDay < shift.EndTime.TimeOfDay)
                            ? !appDetailSlot.Contains(s.Id)
                            : false // Nếu thời gian slot không nằm trong bất kỳ ca nào thì đặt trạng thái thành false
            }).ToList();

            return availableSlots;

            //var slotResponse = new List<SlotResponseModel>();
            //foreach( var app in appDetailSlot)
            //{

            //    var slot = await _context.Slots.SingleOrDefaultAsync(x=>x.Id==app.SlotId);
            //    if (slot==null)
            //        appDetail.Status = "true";
            //    else
            //        appDetail.Status = "false";
            //    var response = new SlotResponseModel()
            //    {
            //        Id = slot.Id,
            //        Time = slot.Time,
            //        Status = appDetail.Status,
            //    };
            //    slotResponse.Add(response);
            //}

            //var slots = await _context.Slots.Where(slot => appDetailSlot.Any(ads => ads.SlotId == slot.Id)).ToListAsync();




        }
    }
}
