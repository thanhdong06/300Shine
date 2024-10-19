﻿using _300Shine.DataAccessLayer.DTO.ResponseModel;
using _300Shine.ResponseType;
using _300Shine.Service.Stylists;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _300Shine.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class StylistController : Controller
    {
        private readonly IStylistService _stylistService;

        public StylistController(IStylistService stylistService)
        {
            _stylistService = stylistService;
        }

        [HttpGet("stylist/slot-by-stylistId")]
        public async Task<ActionResult<JsonResponse<List<SlotResponseModel>>>> GetEmptySlotByStylistId(int? stylistId, int? salonId, int? serviceId, DateTime date)
        {
            try
            {
                var result = await _stylistService.GetEmptySlotByStylistId( stylistId, salonId, serviceId, date);
                return Ok(new JsonResponse<List<SlotResponseModel>> (result, 200, "Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something wrong, please contact with admin", 400, ex.Message));
            }

        }
    }
}