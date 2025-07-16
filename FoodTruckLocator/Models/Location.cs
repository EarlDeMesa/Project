using System;
using System.Collections.Generic;

namespace FoodTruckLocator.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; } = default!; 
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    }
}
