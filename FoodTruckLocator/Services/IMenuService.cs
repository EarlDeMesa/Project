using FoodTruckLocator.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodTruckLocator.Data; 
using FoodTruckLocator.Services;

namespace FoodTruckLocator.Services
{
    public interface IMenuService
    {
        Task<IEnumerable<Menu>> GetAllAsync();
        Task<Menu> GetByIdAsync(int id);
        Task AddAsync(Menu menu);
        Task UpdateAsync(Menu menu);
        Task DeleteAsync(int id);
    }
}
