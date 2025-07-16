using FoodTruckLocator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodTruckLocator.Services
{
    public interface IUserService
    {
        Task<IEnumerable<AppUser>> GetAllAsync();
        Task<AppUser> GetByIdAsync(string id);
        Task AddAsync(AppUser user);
        Task UpdateAsync(AppUser user);
        Task DeleteAsync(string id);
    }
}
