﻿using _300Shine.DataAccessLayer.DTO.RequestModel;
using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.DataAccessLayer.Entities;
using _300Shine.Repository.Repositories.Shift;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Service.Shifts
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository _shiftRepository;

        public ShiftService(IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }

        public async Task<string> CreateShift(ShiftCreateDTO shift)
        {
            return await _shiftRepository.CreateShift(shift);
        }

        public async Task<string> UpdateShift(ShiftUpdateDTO shift)
        {
            return await _shiftRepository.UpdateShift(shift);
        }

        public async Task<string> DeleteShift(int id)
        {
            return await _shiftRepository.DeleteShift(id);
        }

        public async Task<ShiftResponseDTO> GetShiftById(int id)
        {
            return await _shiftRepository.GetShiftById(id);
        }

        public async Task<List<ShiftResponseDTO>> GetShifts(string? search, DateTime? date, string? status, int pageIndex, int pageSize)
        {
            return await _shiftRepository.GetShifts(search, date, status, pageIndex, pageSize);
        }

        public async Task AutoCreateShiftForWholeWeek()
        {
            await _shiftRepository.AutoCreateShiftForWholeWeek();
        }

        public async Task<List<ShiftForChoosingDTO>> GetShiftsBySalonAndStylistId(int salonId, int stylistId)
        {
            return await _shiftRepository.GetShiftsBySalonAndStylistId(salonId, stylistId);
        }

        public async Task<string> ShiftsForStylist(ShiftCreateForStylistDTO request)
        {
            return await _shiftRepository.ShiftsForStylist(request);
        }
    }

}
