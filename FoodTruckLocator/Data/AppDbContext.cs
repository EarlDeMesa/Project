using Microsoft.EntityFrameworkCore;
using FoodTruckLocator.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace FoodTruckLocator.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>

    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<FoodTruck> FoodTrucks { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);

    builder.Entity<AppUser>()
        .HasMany(u => u.Reviews)
        .WithOne(r => r.User)
        .HasForeignKey(r => r.UserID)
        .OnDelete(DeleteBehavior.Cascade); // only ONE cascade

    builder.Entity<FoodTruck>()
        .HasMany(ft => ft.Menus)
        .WithOne(m => m.Truck)
        .HasForeignKey(m => m.TruckID)
        .OnDelete(DeleteBehavior.Cascade);

    builder.Entity<FoodTruck>()
        .HasMany(ft => ft.Schedules)
        .WithOne(s => s.Truck)
        .HasForeignKey(s => s.TruckID)
        .OnDelete(DeleteBehavior.Cascade);

    builder.Entity<FoodTruck>()
        .HasMany(ft => ft.Reviews)
        .WithOne(r => r.Truck)
        .HasForeignKey(r => r.TruckID)
        .OnDelete(DeleteBehavior.Restrict); // this avoids cascade path conflict 

    builder.Entity<Schedule>()
        .HasOne(s => s.Location)
        .WithMany(l => l.Schedules)
        .HasForeignKey(s => s.LocationID)
        .OnDelete(DeleteBehavior.Cascade);
}

    }
}
