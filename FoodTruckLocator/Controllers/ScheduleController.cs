using AutoMapper;
using FoodTruckLocator.Dtos;
using FoodTruckLocator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodTruckLocator.Services;

namespace FoodTruckLocator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly ITruckService _truckService;
        private readonly IMapper _mapper;

        public ScheduleController(IScheduleService scheduleService, ITruckService truckService, IMapper mapper)
        {
            _scheduleService = scheduleService;
            _truckService = truckService;
            _mapper = mapper;
        }

        // Get schedules for a specific truck
        [HttpGet("truck/{truckId}")]
        public async Task<IActionResult> GetByTruck(int truckId)
        {
            var allSchedules = await _scheduleService.GetAllAsync();
            var filteredSchedules = allSchedules.Where(s => s.TruckID == truckId).ToList();

            return Ok(_mapper.Map<List<ScheduleDto>>(filteredSchedules));
        }

        // Create schedule for a truck (only owner or admin allowed)
        [Authorize(Roles = "Owner,Admin")]
        [HttpPost("truck/{truckId}")]
        public async Task<IActionResult> Create(int truckId, CreateScheduleDto dto)
        {
            var truck = await _truckService.GetByIdAsync(truckId);
            if (truck == null) 
                return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRoles = User.FindAll(ClaimTypes.Role).Select(r => r.Value);

            // Owner can only add schedule to their own truck
            if (userRoles.Contains("Owner") && truck.OwnerID != userId)
                return Forbid();

            var schedule = _mapper.Map<Schedule>(dto);
            schedule.TruckID = truckId;

            await _scheduleService.AddAsync(schedule);

            return Ok(_mapper.Map<ScheduleDto>(schedule));
        }
    }

}
