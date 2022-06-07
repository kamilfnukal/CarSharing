using Autofac.Extras.Moq;
using AutoMapper;
using CarSharingBL.Config;
using CarSharingBL.DTOs;
using CarSharingBL.QueryObjects.IQueryObject;
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
    public class CarServiceTests
    {
        private readonly IMapper Mapper = new Mapper(new MapperConfiguration(BLMappingConfig.ConfigureMapping));

        [Fact]
        public async Task TestGetAllCars()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository<Car>>()
                   .Setup(repository => repository.GetAllEntities())
                   .ReturnsAsync(GetMockCars());

                mock.Mock<IMapper>()
                    .Setup(mapper => mapper.Map<IEnumerable<CarDto>>(It.IsAny<IEnumerable<Car>>()))
                    .Returns<IEnumerable<Car>>(cars => Mapper.Map<IEnumerable<CarDto>>(cars));

                var carService = mock.Create<CarService>();
                var result = await carService.GetAllEntities();
                var resultList = new List<CarDto>(result);

                Assert.True(resultList.Count == 3);
                Assert.True(resultList[0].Id == 1 && resultList[1].Id == 2 && resultList[2].Id == 3);
                Assert.True(resultList[0].Brand.Equals("BMW") && resultList[1].Brand.Equals("Audi") && resultList[2].Brand.Equals("Suzuki"));
                Assert.True(resultList.TrueForAll(car => car.Seats == 5));
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task TestGetCarById(int id)
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository<Car>>()
                   .Setup(repository => repository.GetEntityById(id))
                   .ReturnsAsync(GetMockCarById(id));

                mock.Mock<IMapper>()
                    .Setup(mapper => mapper.Map<CarDto>(It.IsAny<Car>()))
                    .Returns<Car>(car => Mapper.Map<CarDto>(car));

                var carService = mock.Create<CarService>();
                var result = await carService.GetEntityByID(id);

                Assert.True(result.Id == id);
                Assert.True(result.Seats == 5);
            }
        }

        [Theory]
        [InlineData(1, "BMW")]
        [InlineData(2, "Audi")]
        [InlineData(3, "Suzuki")]
        public void TestCreateCar(int id, string brand)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var carDto = new CarDto { Id = id, Brand = brand, Seats = 5 };

                mock.Mock<IMapper>()
                    .Setup(mapper => mapper.Map<Car>(It.IsAny<CarDto>()))
                    .Returns<CarDto>(carDto => Mapper.Map<Car>(carDto));

                mock.Mock<IRepository<Car>>()
                   .Setup(repository => repository.CreateEntity(It.IsAny<Car>()))
                   .Returns(CreateMockCar(id, brand));

                var carService = mock.Create<CarService>();
                var result = carService.CreateEntity(carDto);

                Assert.True(result.Id == id);
                Assert.True(result.Brand.Equals(brand) && result.Seats == 5);
            }
        }

        [Theory]
        [InlineData("BMW", 2000)]
        [InlineData("Audi", 2015)]
        public async Task TestGetCarsByBrandAndNewerThan(string brand, int year)
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ICarQueryObject>()
                   .Setup(queryObject => queryObject.GetCarsByBrandAndNewerThan(brand, year))
                   .ReturnsAsync(GetMockCarsByBrandAndYear(brand, year));

                var carService = mock.Create<CarService>();
                var result = await carService.GetCarsByBrandAndNewerThan(brand, year);
                var resultList = new List<CarDto>(result);

                Assert.True(resultList.TrueForAll(car => car.Brand.Equals(brand)));
                Assert.True(resultList.TrueForAll(car => car.YearOfProduction > year));
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task TestGetCarsByUser(int id)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var expected = GetMockCarsByUserId(id);

                mock.Mock<ICarQueryObject>()
                   .Setup(queryObject => queryObject.GetCarsByUser(id))
                   .ReturnsAsync(GetMockCarsByUserId(id));

                var carService = mock.Create<CarService>();
                var result = await carService.GetCarsByUser(id);
                var resultList = new List<CarDto>(result);

                Assert.True(result.Count() == expected.Count());
                Assert.True(resultList.All(car => expected.Any(c => c.Id == car.Id && c.Brand.Equals(car.Brand))));
            }
        }

        [Fact]
        public async Task TestGetCarsWithPhoto()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ICarQueryObject>()
                   .Setup(queryObject => queryObject.GetCarsWithPicture())
                   .ReturnsAsync(GetMockCarDTOs());

                var carService = mock.Create<CarService>();
                var result = await carService.GetCarsWithPicture();

                Assert.True(result.Count() == 3);
                Assert.True(result.All(car => car.Pictures.Count() == 1 && car.Pictures.ToList()[0].Url.Contains("urlObrazka")));
            }
        }

        private List<Car> GetMockCars()
        {
            return new List<Car>()
            {
                new Car { Id = 1, Brand = "BMW", Seats = 5, UserId = 1 },
                new Car { Id = 2, Brand = "Audi", Seats = 5, UserId = 2 },
                new Car { Id = 3, Brand = "Suzuki", Seats = 5, UserId = 2 }
            };
        }

        private List<CarDto> GetMockCarDTOs()
        {
            return new List<CarDto>()
            {
                new CarDto { Id = 1, Brand = "BMW", Seats = 5, UserId = 1, Pictures = new List<PictureGetDto>() { new PictureGetDto() { Id = 1, Url = "urlObrazka1" } } },
                new CarDto { Id = 2, Brand = "Audi", Seats = 5, UserId = 2, Pictures = new List<PictureGetDto>() { new PictureGetDto() { Id = 2, Url = "urlObrazka2" } } },
                new CarDto { Id = 3, Brand = "Suzuki", Seats = 5, UserId = 2, Pictures = new List<PictureGetDto>() { new PictureGetDto() { Id = 3, Url = "urlObrazka3" } } }
            };
        }

        private List<CarDto> GetMockCarsByBrandAndYear(string brand, int year)
        {
            return new List<CarDto>()
            {
                new CarDto { Id = 1, Brand = brand, Seats = 5, YearOfProduction = year + 1 },
                new CarDto { Id = 2, Brand = brand, Seats = 5, YearOfProduction = year + 2 },
                new CarDto { Id = 3, Brand = brand, Seats = 5, YearOfProduction = year + 3 }
            };
        }

        private Car GetMockCarById(int id)
        {
            var cars = GetMockCars();
            return cars.First(car => car.Id == id);
        }

        private Car CreateMockCar(int id, string brand)
        {
            return new Car { Id = id, Brand = brand, Seats = 5 };
        }

        private List<CarDto> GetMockCarsByUserId(int id)
        {
            var cars = GetMockCarDTOs();
            var result = new List<CarDto>();

            switch (id)
            {
                case 1:
                    result.Add(cars[0]);
                    break;
                case 2:
                    result.AddRange(new List<CarDto>() {cars[1], cars[2]});
                    break;
                default:
                    break;
            }
            return result;
        }


    }
}
