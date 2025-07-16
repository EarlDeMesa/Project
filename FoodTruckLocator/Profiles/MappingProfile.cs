using AutoMapper;
using FoodTruckLocator.Dtos;
using FoodTruckLocator.Models;

namespace FoodTruckLocator.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User Mappings
            CreateMap<AppUser, UserDto>();
            CreateMap<RegisterDto, AppUser>();

            // FoodTruck Mappings
            CreateMap<FoodTruck, FoodTruckDto>();
            CreateMap<CreateFoodTruckDto, FoodTruck>();

            CreateMap<FoodTruck, FoodTruckDto>().ReverseMap();
            
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<CreateMenuDto, Menu>();

            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<CreateLocationDto, Location>();

            CreateMap<Schedule, ScheduleDto>().ReverseMap();
            CreateMap<CreateScheduleDto, Schedule>();

            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<CreateReviewDto, Review>();


        }
    }
}
