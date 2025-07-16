using AutoMapper;
using FoodTruckLocator.Dtos;
using FoodTruckLocator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTruckLocator.Services;

namespace FoodTruckLocator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodTruckController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFoodTruckService _foodTruckService;

        public FoodTruckController(
            IMapper mapper,
            UserManager<AppUser> userManager,
            IFoodTruckService foodTruckService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _foodTruckService = foodTruckService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var trucks = await _foodTruckService.GetAllAsync();
            var dtoList = _mapper.Map<List<FoodTruckDto>>(trucks);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var truck = await _foodTruckService.GetByIdAsync(id);
            if (truck == null) return NotFound();

            return Ok(_mapper.Map<FoodTruckDto>(truck));
        }

        [Authorize(Roles = "Owner,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFoodTruckDto dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return Forbid();

            // Confirm user has Owner or Admin role before allowing creation
            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Owner") && !roles.Contains("Admin"))
                return Forbid();

            var truck = _mapper.Map<FoodTruck>(dto);
            truck.OwnerID = userId;  // Set the current user as owner

            await _foodTruckService.AddAsync(truck);

            return CreatedAtAction(nameof(GetById), new { id = truck.FoodTruckID }, _mapper.Map<FoodTruckDto>(truck));
        }

        [Authorize(Roles = "Owner,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateFoodTruckDto dto)
        {
            var truck = await _foodTruckService.GetByIdAsync(id);
            if (truck == null) return NotFound();

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // Only allow update if user owns truck or is an admin
            if (truck.OwnerID != userId && !(await IsUserAdminAsync(userId)))
                return Forbid();

            _mapper.Map(dto, truck);
            await _foodTruckService.UpdateAsync(truck);

            return NoContent();
        }

        [Authorize(Roles = "Owner,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var truck = await _foodTruckService.GetByIdAsync(id);
            if (truck == null) return NotFound();

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // Only allow delete if user owns truck or is an admin
            if (truck.OwnerID != userId && !(await IsUserAdminAsync(userId)))
                return Forbid();

            await _foodTruckService.DeleteAsync(id);

            return Ok(new { message = "FoodTruck deleted successfully", foodTruckID = id });
        }

        private async Task<bool> IsUserAdminAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var roles = await _userManager.GetRolesAsync(user);
            return roles.Contains("Admin");
        }
    }
}
