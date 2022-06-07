using AutoMapper;
using CarSharingBL.DTOs;
using CarSharingDAL.Entities;
using System;

namespace CarSharingBL.Config
{
    public class BLMappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            //Car
            config.CreateMap<Car, CarCreateDto>().ReverseMap();
            config.CreateMap<Car, CarDto>().ReverseMap();
            config.CreateMap<Car, CarOfRideDto>().ReverseMap();

            //User
            config.CreateMap<User, DriverDto>()
                .ForMember(driverDto => driverDto.Age, d => d.MapFrom(user => GetAge(user.BirthDate)))
                .ReverseMap();
            config.CreateMap<User, DriverOfRideDto>().ReverseMap();
            config.CreateMap<User, UserDto>().ReverseMap();
            config.CreateMap<User, UserEditDto>().ReverseMap();
            config.CreateMap<User, UserLoginDto>().ReverseMap();
            config.CreateMap<User, UserRegistrationDto>()
                .ForMember(userRegistrationDto => userRegistrationDto.BirthDate, u => u.MapFrom(user => user.BirthDate.ToString()))
                .ReverseMap();

            //Passenger
            config.CreateMap<Passenger, PassengerCreateDto>().ReverseMap();
            config.CreateMap<Passenger, PassengerDto>().ReverseMap();

            //Picture
            config.CreateMap<Picture, PictureDto>().ReverseMap();
            config.CreateMap<Picture, PictureGetDto>().ReverseMap();
            config.CreateMap<PictureDto, PictureGetDto>().ReverseMap();

            //Rating
            config.CreateMap<Rating, RatingDto>().ReverseMap();

            //Ride
            config.CreateMap<Ride, RideCreateDto>().ReverseMap();
            config.CreateMap<Ride, RideDto>()
                .ReverseMap();
        }

        private static int GetAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
