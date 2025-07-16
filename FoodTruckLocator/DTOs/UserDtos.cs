using System.ComponentModel.DataAnnotations;

namespace FoodTruckLocator.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = default!;

        [Required(ErrorMessage = "UserType is required")]
        [RegularExpression("^(Admin|Customer|Owner)$", ErrorMessage = "UserType must be Admin, Customer, or Owner")]
        public string UserType { get; set; } = default!;
    }

    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = default!;
    }

    public class UserDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string UserType { get; set; } = default!;
    }
}
