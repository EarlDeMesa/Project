using FoodTruckLocator.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodTruckLocator.Data; 
using FoodTruckLocator.Services;

namespace FoodTruckLocator.Services
{
    public class LocationService : ILocationService
    {
        private readonly AppDbContext _context;

        public LocationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await _context.Locations
                .Include(l => l.Schedules)
                .ToListAsync();
        }

        public async Task<Location> GetByIdAsync(int id)
        {
            return await _context.Locations
                .Include(l => l.Schedules)
                .FirstOrDefaultAsync(l => l.LocationID == id);
        }

        public async Task AddAsync(Location location)
        {
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Location location)
        {
            _context.Locations.Update(location);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location != null)
            {
                _context.Locations.Remove(location);
                await _context.SaveChangesAsync();
            }
        }
    }
}
