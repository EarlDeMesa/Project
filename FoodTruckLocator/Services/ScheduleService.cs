using FoodTruckLocator.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodTruckLocator.Data; 
using FoodTruckLocator.Services;

namespace FoodTruckLocator.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly AppDbContext _context;

        public ScheduleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Schedule>> GetAllAsync()
        {
            return await _context.Schedules
                .Include(s => s.Truck)
                .Include(s => s.Location)
                .ToListAsync();
        }

        public async Task<Schedule> GetByIdAsync(int id)
        {
            return await _context.Schedules
                .Include(s => s.Truck)
                .Include(s => s.Location)
                .FirstOrDefaultAsync(s => s.ScheduleID == id);
        }

        public async Task AddAsync(Schedule schedule)
        {
            await _context.Schedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Schedule schedule)
        {
            _context.Schedules.Update(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }
        }
    }
}
