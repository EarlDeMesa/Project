using System;

namespace FoodTruckLocator.Models
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public int TruckID { get; set; }
        public FoodTruck Truck { get; set; } = default!;
        public int LocationID { get; set; }
        public Location Location { get; set; } = default!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
