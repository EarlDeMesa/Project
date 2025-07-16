using AutoMapper;
using FoodTruckLocator.Dtos;
using FoodTruckLocator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FoodTruckLocator.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodTruckLocator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;

        public MenuController(IMenuService menuService, IMapper mapper)
        {
            _menuService = menuService;
            _mapper = mapper;
        }

        [HttpGet("truck/{truckId}")]
        public async Task<IActionResult> GetByTruck(int truckId)
        {
            var menus = await _menuService.GetAllAsync();
            var filtered = new List<Menu>();
            foreach (var m in menus)
                if (m.TruckID == truckId)
                    filtered.Add(m);

            return Ok(_mapper.Map<List<MenuDto>>(filtered));
        }

        [Authorize(Roles = "Owner,Admin")]
        [HttpPost("truck/{truckId}")]
        public async Task<IActionResult> Create(int truckId, CreateMenuDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Ownership check needed here (not done yet)

            var menu = _mapper.Map<Menu>(dto);
            menu.TruckID = truckId;

            await _menuService.AddAsync(menu);

            return Ok(_mapper.Map<MenuDto>(menu));
        }

        [Authorize(Roles = "Owner,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateMenuDto dto)
        {
            var menu = await _menuService.GetByIdAsync(id);
            if (menu == null)
                return NotFound();

            // Ownership check needed here

            _mapper.Map(dto, menu);
            await _menuService.UpdateAsync(menu);

            return NoContent();
        }

        [Authorize(Roles = "Owner,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var menu = await _menuService.GetByIdAsync(id);
            if (menu == null)
                return NotFound();

            // Ownership check needed here

            await _menuService.DeleteAsync(id);

            return NoContent();
        }
    }
}
