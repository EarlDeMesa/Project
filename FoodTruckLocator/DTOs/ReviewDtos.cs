namespace FoodTruckLocator.Dtos
{
    public class ReviewDto
    {
        public int ReviewID { get; set; }
        public int TruckID { get; set; }
        public string UserID { get; set; } = default!;
        public int Rating { get; set; }
        public string Comment { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }

    public class CreateReviewDto
    {
        public int TruckID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = default!;
    }
}
