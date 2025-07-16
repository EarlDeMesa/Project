namespace FoodTruckLocator.Dtos
{
    public class LocationDto
    {
        public int LocationID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; } = default!;
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class CreateLocationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; } = default!;
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
