using Autofac.Extras.Moq;
using AutoMapper;
using CarSharingBL.Config;
using CarSharingBL.DTOs;
using CarSharingBL.Services.Service;
using CarSharingDAL.Entities;
using CarSharingInfrastructure.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CarSharingTests
{
    public class RideServiceTests
    {
        private readonly IMapper Mapper = new Mapper(new MapperConfiguration(BLMappingConfig.ConfigureMapping));

        [Fact]
        public async Task TestGetAllRides()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository<Ride>>()
                   .Setup(repository => repository.GetAllEntities())
                   .ReturnsAsync(GetMockRides());

                mock.Mock<IMapper>()
                    .Setup(mapper => mapper.Map<IEnumerable<RideDto>>(It.IsAny<IEnumerable<Ride>>()))
                    .Returns<IEnumerable<Ride>>(ride => Mapper.Map<IEnumerable<RideDto>>(ride));

                var rideService = mock.Create<RideService>();
                var result = await rideService.GetAllEntities();
                var resultList = new List<RideDto>(result);

                Assert.True(resultList.Count == 3);
                Assert.True(resultList[0].Id == 1 && resultList[1].Id == 2 && resultList[2].Id == 3);
                Assert.True(resultList[0].Price == 200 && resultList[1].Price == 500 && resultList[2].Price == 400);
                Assert.True(resultList[0].AvailableSeats == 3 && resultList[1].AvailableSeats == 2 && resultList[2].AvailableSeats == 4);
                Assert.True(resultList[0].DriverId == 1 && resultList[1].DriverId == 10 && resultList[2].DriverId == 8);
                Assert.True(resultList.TrueForAll(ride => ride.DateTime.Equals(DateTime.Parse("Feb 20, 2021"))));
                Assert.True(resultList.TrueForAll(ride => ride.CityFrom.Equals("Bratislava")));
                Assert.True(resultList[0].CityTo.Equals("Brno") && resultList[1].CityTo.Equals("Praha") && resultList[2].CityTo.Equals("Zvolen"));
                Assert.True(resultList[0].CarId == 10 && resultList[1].CarId == 20 && resultList[2].CarId == 11);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task TestGetRideById(int id)
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository<Ride>>()
                   .Setup(repository => repository.GetEntityById(id))
                   .ReturnsAsync(GetMockRideById(id));

                mock.Mock<IMapper>()
                    .Setup(mapper => mapper.Map<RideDto>(It.IsAny<Ride>()))
                    .Returns<Ride>(ride => Mapper.Map<RideDto>(ride));

                var rideService = mock.Create<RideService>();
                var result = await rideService.GetEntityByID(id);

                Assert.True(result.Id == id);
                Assert.True(result.DateTime.Equals(DateTime.Parse("Feb 20, 2021")));
                Assert.True(result.CityFrom.Equals("Bratislava"));
            }
        }

        [Theory]
        [InlineData(1, 500, 4, "Malacky")]
        [InlineData(2, 600, 2, "Martin")]
        [InlineData(3, 450, 1, "Pezinok")]
        public void TestCreateRide(int id, int price, int availableSeats, string cityTo)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var rideDto = new RideDto { Id = id, Price = price, AvailableSeats = availableSeats, DriverId = 14, DateTime = DateTime.Parse("Feb 22, 2021"), CityFrom = "Bratislava", CityTo = cityTo, CarId = 18 };

                mock.Mock<IMapper>()
                    .Setup(mapper => mapper.Map<Ride>(It.IsAny<RideDto>()))
                    .Returns<RideDto>(rideDto => Mapper.Map<Ride>(rideDto));

                mock.Mock<IRepository<Ride>>()
                   .Setup(repository => repository.CreateEntity(It.IsAny<Ride>()))
                   .Returns(CreateMockRide(id, price, availableSeats, cityTo));

                var rideService = mock.Create<RideService>();
                var result = rideService.CreateEntity(rideDto);

                Assert.True(result.Id == id);
                Assert.True(result.Price == price && result.AvailableSeats == availableSeats && result.CityTo.Equals(cityTo));
                Assert.True(result.DriverId == 14 && result.DateTime.Equals(DateTime.Parse("Feb 22, 2021")) && result.CityFrom.Equals("Bratislava") && result.CarId == 18);
            }
        }

        private List<Ride> GetMockRides()
        {
            return new List<Ride>()
            {
                new Ride { Id = 1, Price = 200, AvailableSeats = 3, DriverId = 1, DateTime = DateTime.Parse("Feb 20, 2021"), CityFrom = "Bratislava", CityTo = "Brno", CarId = 10 },
                new Ride { Id = 2, Price = 500, AvailableSeats = 2, DriverId = 10, DateTime = DateTime.Parse("Feb 20, 2021"), CityFrom = "Bratislava", CityTo = "Praha", CarId = 20 },
                new Ride { Id = 3, Price = 400, AvailableSeats = 4, DriverId = 8, DateTime = DateTime.Parse("Feb 20, 2021"), CityFrom = "Bratislava", CityTo = "Zvolen", CarId = 11 }
            };
        }

        private Ride GetMockRideById(int id)
        {
            var rides = GetMockRides();
            return rides.First(ride => ride.Id == id);
        }

        private Ride CreateMockRide(int id, int price, int availableSeats, string cityTo)
        {
            return new Ride { Id = id, Price = price, AvailableSeats = availableSeats, DriverId = 14, DateTime = DateTime.Parse("Feb 22, 2021"), CityFrom = "Bratislava", CityTo = cityTo, CarId = 18 };
        }
    }
}
