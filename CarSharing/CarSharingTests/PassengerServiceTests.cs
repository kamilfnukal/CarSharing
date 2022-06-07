using Autofac.Extras.Moq;
using AutoMapper;
using CarSharingBL.Config;
using CarSharingBL.DTOs;
using CarSharingBL.Services.Service;
using CarSharingDAL.Entities;
using CarSharingInfrastructure.Repository;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CarSharingTests
{
    public class PassengerServiceTests
    {
        private readonly IMapper Mapper = new Mapper(new MapperConfiguration(BLMappingConfig.ConfigureMapping));

        [Fact]
        public async Task TestGetAllPassenger()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository<Passenger>>()
                   .Setup(repository => repository.GetAllEntities())
                   .ReturnsAsync(GetMockPassengers());

                mock.Mock<IMapper>()
                    .Setup(mapper => mapper.Map<IEnumerable<PassengerDto>>(It.IsAny<IEnumerable<Passenger>>()))
                    .Returns<IEnumerable<Passenger>>(passenger => Mapper.Map<IEnumerable<PassengerDto>>(passenger));

                var passengerService = mock.Create<PassengerService>();
                var result = await passengerService.GetAllEntities();
                var resultList = new List<PassengerDto>(result);

                Assert.True(resultList.Count == 3);
                Assert.True(resultList[0].Id == 1 && resultList[1].Id == 2 && resultList[2].Id == 3);
                Assert.True(resultList[0].UserId == 45 && resultList[1].UserId == 53 && resultList[2].UserId == 22);
                Assert.True(resultList[0].RideId == 6 && resultList[1].RideId == 7 && resultList[2].RideId == 8);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task TestGetPassengerById(int id)
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository<Passenger>>()
                   .Setup(repository => repository.GetEntityById(id))
                   .ReturnsAsync(GetMockPassengerById(id));
                
                mock.Mock<IMapper>()
                    .Setup(mapper => mapper.Map<PassengerDto>(It.IsAny<Passenger>()))
                    .Returns<Passenger>(passenger => Mapper.Map<PassengerDto>(passenger));

                var passengerService = mock.Create<PassengerService>();
                var result = await passengerService.GetEntityByID(id);

                Assert.True(result.Id == id);
                Assert.True(result.RideId == id + 5);
            }
        }

        [Theory]
        [InlineData(1, 45, 6)]
        [InlineData(2, 32, 12)]
        [InlineData(3, 62, 8)]
        public void TestCreatePassenger(int id, int userId, int rideId)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var passengerDto = new PassengerDto { Id = id, UserId = userId, RideId = rideId };

                mock.Mock<IMapper>()
                    .Setup(mapper => mapper.Map<Passenger>(It.IsAny<PassengerDto>()))
                    .Returns<PassengerDto>(passengerDto => Mapper.Map<Passenger>(passengerDto));

                mock.Mock<IRepository<Passenger>>()
                   .Setup(repository => repository.CreateEntity(It.IsAny<Passenger>()))
                   .Returns(CreateMockPassenger(id, userId, rideId));

                var passengerService = mock.Create<PassengerService>();
                var result = passengerService.CreateEntity(passengerDto);

                Assert.True(result.Id == id);
                Assert.True(result.UserId == userId && result.RideId == rideId);
            }
        }

        private List<Passenger> GetMockPassengers()
        {
            return new List<Passenger>()
            {
                new Passenger { Id = 1, UserId = 45, RideId = 6 },
                new Passenger { Id = 2, UserId = 53, RideId = 7 },
                new Passenger { Id = 3, UserId = 22, RideId = 8 }
            };
        }

        private Passenger GetMockPassengerById(int id)
        {
            var passengers = GetMockPassengers();
            return passengers.First(passenger => passenger.Id == id);
        }

        private Passenger CreateMockPassenger(int id, int userId, int rideId)
        {
            return new Passenger { Id = id, UserId = userId, RideId = rideId };
        }
    }
}
