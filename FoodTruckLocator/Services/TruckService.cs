using FoodTruckLocator.Models;
using FoodTruckLocator.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FoodTruckLocator.Services
{
    public class TruckService : ITruckService
    {
        private readonly AppDbContext _context;

        public TruckService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FoodTruck> GetByIdAsync(int id)
        {
            return await _context.FoodTrucks.FindAsync(id);
        }
    }
}
