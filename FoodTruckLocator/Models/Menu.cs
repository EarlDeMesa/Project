using System;

namespace FoodTruckLocator.Models
{
    public class Menu
    {
        public int MenuID { get; set; }
        public int TruckID { get; set; }
        public FoodTruck Truck { get; set; } = default!;  
        public string Name { get; set; } = default!;      
        public string Description { get; set; } = default!; 
        public decimal Price { get; set; }
    }
}
