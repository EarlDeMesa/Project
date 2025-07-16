using FoodTruckLocator.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodTruckLocator.Data; 
using FoodTruckLocator.Services;
namespace FoodTruckLocator.Services
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> GetAllAsync();
        Task<Schedule> GetByIdAsync(int id);
        Task AddAsync(Schedule schedule);
        Task UpdateAsync(Schedule schedule);
        Task DeleteAsync(int id);
    }
}
