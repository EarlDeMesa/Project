namespace FoodTruckLocator.Dtos
{
    public class MenuDto
    {
        public int MenuID { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
    }

    public class CreateMenuDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
    }
}
