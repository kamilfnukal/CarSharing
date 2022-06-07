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
    public class PictureServiceTests
    {
        private readonly IMapper Mapper = new Mapper(new MapperConfiguration(BLMappingConfig.ConfigureMapping));

        [Fact]
        public async Task TestGetAllPictures()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository<Picture>>()
                   .Setup(repository => repository.GetAllEntities())
                   .ReturnsAsync(GetMockPictures());

                mock.Mock<IMapper>()
                    .Setup(mapper => mapper.Map<IEnumerable<PictureDto>>(It.IsAny<IEnumerable<Picture>>()))
                    .Returns<IEnumerable<Picture>>(picture => Mapper.Map<IEnumerable<PictureDto>>(picture));

                var pictureService = mock.Create<PictureService>();
                var result = await pictureService.GetAllEntities();
                var resultList = new List<PictureDto>(result);

                Assert.True(resultList.Count == 3);
                Assert.True(resultList[0].Id == 1 && resultList[1].Id == 2 && resultList[2].Id == 3);
                Assert.True(resultList.All(picture => picture.Url.Contains("urlObrazka")));
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
                mock.Mock<IRepository<Picture>>()
                   .Setup(repository => repository.GetEntityById(id))
                   .ReturnsAsync(GetMockPictureById(id));

                mock.Mock<IMapper>()
                    .Setup(mapper => mapper.Map<PictureDto>(It.IsAny<Picture>()))
                    .Returns<Picture>(picture => Mapper.Map<PictureDto>(picture));

                var pictureService = mock.Create<PictureService>();
                var result = await pictureService.GetEntityByID(id);

                Assert.True(result.Id == id);
                Assert.Contains("urlObrazka", result.Url);
            }
        }

        [Theory]
        [InlineData(1, "urlSuperObrazka")]
        [InlineData(2, "urlSuperDuperObrazka")]
        [InlineData(3, "urlMegaObrazka")]
        public void TestCreatePicture(int id, string url)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var pictureDto = new PictureDto { Id = id, Url = "urlObrazka" };

                mock.Mock<IMapper>()
                    .Setup(mapper => mapper.Map<Picture>(It.IsAny<PictureDto>()))
                    .Returns<PictureDto>(pictureDto => Mapper.Map<Picture>(pictureDto));

                mock.Mock<IRepository<Picture>>()
                   .Setup(repository => repository.CreateEntity(It.IsAny<Picture>()))
                   .Returns(CreateMockPicture(id, url));

                var pictureService = mock.Create<PictureService>();
                var result = pictureService.CreateEntity(pictureDto);

                Assert.True(result.Id == id);
                Assert.True(result.Url.Equals(url));
            }
        }

        private List<Picture> GetMockPictures()
        {
            return new List<Picture>()
            {
                new Picture { Id = 1, Url = "urlObrazka1" },
                new Picture { Id = 2, Url = "urlObrazka2" },
                new Picture { Id = 3, Url = "urlObrazka3" }
            };
        }

        private Picture GetMockPictureById(int id)
        {
            var pictures = GetMockPictures();
            return pictures.First(picture => picture.Id == id);
        }

        private Picture CreateMockPicture(int id, string url)
        {
            return new Picture { Id = id, Url = url };
        }
    }
}
