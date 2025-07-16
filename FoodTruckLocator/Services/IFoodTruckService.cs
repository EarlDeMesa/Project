using FoodTruckLocator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodTruckLocator.Services
{
    public interface IFoodTruckService
    {
        Task<IEnumerable<FoodTruck>> GetAllAsync();
        Task<FoodTruck> GetByIdAsync(int id);
        Task AddAsync(FoodTruck foodTruck);
        Task UpdateAsync(FoodTruck foodTruck);
        Task DeleteAsync(int id);
    }
}
