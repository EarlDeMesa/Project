using FoodTruckLocator.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodTruckLocator.Data; 
using FoodTruckLocator.Services;

namespace FoodTruckLocator.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review> GetByIdAsync(int id);
        Task AddAsync(Review review);
        Task UpdateAsync(Review review);
        Task DeleteAsync(int id);
    }
}
