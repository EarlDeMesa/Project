using System.Collections.Generic;

namespace FoodTruckLocator.Models
{
    public class FoodTruck
    {
        public int FoodTruckID { get; set; }
        
        public string OwnerID { get; set; } = default!;
        public AppUser Owner { get; set; } = default!;
        
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string CuisineType { get; set; } = default!;
        
        public ICollection<Menu> Menus { get; set; } = new List<Menu>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    }
}
