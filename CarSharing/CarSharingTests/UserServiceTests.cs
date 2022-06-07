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
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarSharingTests
{
    public class UserServiceTests
    {
        private readonly IMapper Mapper = new Mapper(new MapperConfiguration(BLMappingConfig.ConfigureMapping));

        [Fact]
        public async Task TestGetAllUsers()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository<User>>()
                   .Setup(repository => repository.GetAllEntities())
                   .ReturnsAsync(GetMockUsers());

                mock.Mock<IMapper>()
                    .Setup(mapper => mapper.Map<IEnumerable<UserDto>>(It.IsAny<IEnumerable<User>>()))
                    .Returns<IEnumerable<User>>(user => Mapper.Map<IEnumerable<UserDto>>(user));

                var userService = mock.Create<UserService>();
                var result = await userService.GetAllEntities();
                var resultList = new List<UserDto>(result);

                Assert.True(resultList.Count == 3);
                Assert.True(resultList[0].UserName.Equals("patkajesaman") && resultList[1].UserName.Equals("Dalibor") && resultList[2].UserName.Equals("MEMELORD"));
                Assert.True(resultList[0].Name.Equals("Patka") && resultList[1].Name.Equals("Dalibor") && resultList[2].Name.Equals("Kamil"));
                Assert.True(resultList[0].Surname.Equals("Andicsova") && resultList[1].Surname.Equals("Pantlik") && resultList[2].Surname.Equals("Fnukal"));
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task TestGetUserById(int id)
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository<User>>()
                   .Setup(repository => repository.GetEntityById(id))
                   .ReturnsAsync(GetMockUserById(id));

                mock.Mock<IMapper>()
                    .Setup(mapper => mapper.Map<UserDto>(It.IsAny<User>()))
                    .Returns<User>(user => Mapper.Map<UserDto>(user));

                var userService = mock.Create<UserService>();
                var result = await userService.GetEntityByID(id);

                Assert.True(result.Id == id);
            }
        }

        [Theory]
        [InlineData(1, "pikaPika", "Ivan", "Vanat")]
        [InlineData(2, "Pista", "Stefan", "Bojnak")]
        [InlineData(3, "StyleZ", "Dominik", "Laso")]
        public void TestCreatePicture(int id, string userName, string name, string surname)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var userDto = new UserDto { Id = id, UserName = userName, Name = name, Surname = surname, BirthDate = DateTime.Parse("Jan 11, 1999") };

                mock.Mock<IMapper>()
                    .Setup(mapper => mapper.Map<User>(It.IsAny<UserDto>()))
                    .Returns<UserDto>(userDto => Mapper.Map<User>(userDto));

                mock.Mock<IRepository<User>>()
                   .Setup(repository => repository.CreateEntity(It.IsAny<User>()))
                   .Returns(CreateMockUser(id, userName, name, surname));

                var userService = mock.Create<UserService>();
                var result = userService.CreateEntity(userDto);

                Assert.True(result.Id == id);
                Assert.True(result.UserName.Equals(userName) && result.Name.Equals(name) && result.Surname.Equals(surname));
                Assert.True(result.BirthDate.Equals(DateTime.Parse("Jan 11, 1999")));
            }
        }

        private List<User> GetMockUsers()
        {
            return new List<User>()
            {
                new User { Id = 1, UserName = "patkajesaman", Name = "Patka", Surname = "Andicsova" },
                new User { Id = 2, UserName = "Dalibor", Name = "Dalibor", Surname = "Pantlik" },
                new User { Id = 3, UserName = "MEMELORD", Name = "Kamil", Surname = "Fnukal" }
            };
        }

        private User GetMockUserById(int id)
        {
            var users = GetMockUsers();
            return users.First(user => user.Id == id);
        }

        private User CreateMockUser(int id, string userName, string name, string surname)
        {
            return new User { Id = id, UserName = userName, Name = name, Surname = surname, BirthDate = DateTime.Parse("Jan 11, 1999") };
        }
    }
}
