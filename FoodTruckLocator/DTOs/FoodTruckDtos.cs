namespace FoodTruckLocator.Dtos
{
    public class FoodTruckDto
    {
        public int FoodTruckID { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string CuisineType { get; set; } = default!;
    }

    public class CreateFoodTruckDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string CuisineType { get; set; } = default!;
    }
}
