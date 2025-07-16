using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FoodTruckLocator.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = default!;
        // removed the usertype
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
