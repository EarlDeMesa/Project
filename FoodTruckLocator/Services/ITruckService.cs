using FoodTruckLocator.Models;
using System.Threading.Tasks;

namespace FoodTruckLocator.Services
{
    public interface ITruckService
    {
        Task<FoodTruck> GetByIdAsync(int id);
    }
}
