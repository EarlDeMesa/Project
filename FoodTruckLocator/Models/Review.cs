using System;

namespace FoodTruckLocator.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public int TruckID { get; set; }
        public FoodTruck Truck { get; set; } = default!;
        public string UserID { get; set; } = default!;
        public AppUser User { get; set; } = default!;
        public int Rating { get; set; }
        public string Comment { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
