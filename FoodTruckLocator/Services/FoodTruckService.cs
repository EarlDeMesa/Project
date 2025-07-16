using FoodTruckLocator.Models;
using FoodTruckLocator.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodTruckLocator.Services
{
    public class FoodTruckService : IFoodTruckService
    {
        private readonly AppDbContext _context;

        public FoodTruckService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FoodTruck>> GetAllAsync()
        {
            return await _context.FoodTrucks
                .Include(ft => ft.Menus)
                .Include(ft => ft.Schedules)
                .Include(ft => ft.Reviews)
                .ToListAsync();
        }

        public async Task<FoodTruck> GetByIdAsync(int id)
        {
            return await _context.FoodTrucks
                .Include(ft => ft.Menus)
                .Include(ft => ft.Schedules)
                .Include(ft => ft.Reviews)
                .FirstOrDefaultAsync(ft => ft.FoodTruckID == id);
        }

        public async Task AddAsync(FoodTruck foodTruck)
        {
            await _context.FoodTrucks.AddAsync(foodTruck);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FoodTruck foodTruck)
        {
            _context.FoodTrucks.Update(foodTruck);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FoodTrucks.FindAsync(id);
            if (entity != null)
            {
                _context.FoodTrucks.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
