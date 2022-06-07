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
    public class RatingServiceTests
    {
        private readonly IMapper Mapper = new Mapper(new MapperConfiguration(BLMappingConfig.ConfigureMapping));

        [Fact]
        public async Task TestGetAllRatings()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository<Rating>>()
                   .Setup(repository => repository.GetAllEntities())
                   .ReturnsAsync(GetMockRatings());

                mock.Mock<IMapper>()
                    .Setup(mapper => mapper.Map<IEnumerable<RatingDto>>(It.IsAny<IEnumerable<Rating>>()))
                    .Returns<IEnumerable<Rating>>(rating => Mapper.Map<IEnumerable<RatingDto>>(rating));

                var ratingService = mock.Create<RatingService>();
                var result = await ratingService.GetAllEntities();
                var resultList = new List<RatingDto>(result);

                Assert.True(resultList.Count == 3);
                Assert.True(resultList[0].Id == 1 && resultList[1].Id == 2 && resultList[2].Id == 3);
                Assert.True(resultList[0].Rate == 5 && resultList[1].Rate == 3 && resultList[2].Rate == 4);
                Assert.True(resultList.All(rating => rating.ForUserId == 2));
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task TestGetPictureById(int id)
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository<Rating>>()
                   .Setup(repository => repository.GetEntityById(id))
                   .ReturnsAsync(GetMockRatingById(id));

                mock.Mock<IMapper>()
                    .Setup(mapper => mapper.Map<RatingDto>(It.IsAny<Rating>()))
                    .Returns<Rating>(rating => Mapper.Map<RatingDto>(rating));

                var ratingService = mock.Create<RatingService>();
                var result = await ratingService.GetEntityByID(id);

                Assert.True(result.Id == id);
                Assert.True(result.ForUserId == 2);
            }
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void TestCreatePicture(int id, int rate)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var ratingDto = new RatingDto { Id = id, Rate = rate, ForUserId = 7 };

                mock.Mock<IMapper>()
                    .Setup(mapper => mapper.Map<Rating>(It.IsAny<RatingDto>()))
                    .Returns<RatingDto>(ratingDto => Mapper.Map<Rating>(ratingDto));

                mock.Mock<IRepository<Rating>>()
                   .Setup(repository => repository.CreateEntity(It.IsAny<Rating>()))
                   .Returns(CreateMockRating(id, rate));

                var ratingService = mock.Create<RatingService>();
                var result = ratingService.CreateEntity(ratingDto);

                Assert.True(result.Id == id);
                Assert.True(result.Rate == rate);
                Assert.True(result.ForUserId == 7);
            }
        }

        private List<Rating> GetMockRatings()
        {
            return new List<Rating>()
            {
                new Rating { Id = 1, Rate = 5, ForUserId = 2 },
                new Rating { Id = 2, Rate = 3, ForUserId = 2 },
                new Rating { Id = 3, Rate = 4, ForUserId = 2 }
            };
        }

        private Rating GetMockRatingById(int id)
        {
            var ratings = GetMockRatings();
            return ratings.First(rating => rating.Id == id);
        }

        private Rating CreateMockRating(int id, int rate)
        {
            return new Rating { Id = id, Rate = rate, ForUserId = 7 };
        }
    }
}
