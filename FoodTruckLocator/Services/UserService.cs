using FoodTruckLocator.Models;
using FoodTruckLocator.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodTruckLocator.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await _context.AppUsers.ToListAsync();
        }

        public async Task<AppUser> GetByIdAsync(string id)
        {
            return await _context.AppUsers.FindAsync(id);
        }

        public async Task AddAsync(AppUser user)
        {
            await _context.AppUsers.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AppUser user)
        {
            _context.AppUsers.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _context.AppUsers.FindAsync(id);
            if (user != null)
            {
                _context.AppUsers.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
