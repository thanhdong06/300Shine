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

        public async Task<List<SlotResponseModel>> GetEmptySlotByStylistId(int stylistId)
        {
            //var stylist = await _context.Stylists.SingleOrDefaultAsync(s=>s.Id == stylistId);
            //if(stylist == null)
            //    throw new Exception("Stylist is not found");
            IQueryable<SlotEntity> slots = _context.Slots.Where(x => x.Id != null && x.IsDeleted == false);
            var appDetail = await _context.AppointmentDetails.SingleOrDefaultAsync(s=>s.StylistId==stylistId);
            if (appDetail == null)
                throw new Exception("Stylist of appointment not found");
            var appDetailSlot = await _context.AppointmentSlots.Where(x => x.AppointmentDetailId == appDetail.Id).Select(ads => ads.SlotId)
            .ToListAsync();
            if (appDetailSlot == null) throw new Exception("Appointment Detail not found");
            var availableSlots = slots.Select(s => new SlotResponseModel
            {
                Id = s.Id,
                Time = s.Time,
                Status = !appDetailSlot.Contains(s.Id)
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
