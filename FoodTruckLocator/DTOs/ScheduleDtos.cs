namespace FoodTruckLocator.Dtos
{
    public class ScheduleDto
    {
        public int ScheduleID { get; set; }
        public int TruckID { get; set; }
        public int LocationID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class CreateScheduleDto
    {
        public int LocationID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
