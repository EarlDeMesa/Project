using AutoMapper;
using FoodTruckLocator.Dtos;
using FoodTruckLocator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FoodTruckLocator.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodTruckLocator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public LocationController(
            ILocationService locationService,
            IMapper mapper,
            UserManager<AppUser> userManager)
        {
            _locationService = locationService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var locations = await _locationService.GetAllAsync();
            var dtoList = _mapper.Map<List<LocationDto>>(locations);
            return Ok(dtoList);
        }

        [Authorize(Roles = "Owner,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateLocationDto dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return Forbid();

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Owner") && !roles.Contains("Admin"))
                return Forbid();

            var location = _mapper.Map<Location>(dto);
            await _locationService.AddAsync(location);

            return Ok(_mapper.Map<LocationDto>(location));
        }
    }
}
