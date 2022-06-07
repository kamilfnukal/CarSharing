using CarSharingBL.DTOs;
using CarSharingDAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Services.IService
{
    public interface IUserService : IBaseService<User, UserDto>
    {
        Task<DriverDto> GetDriverById(int driverId);

        Task<UserDto> GetUserByUserName(string userName);

        Task<ICollection<UserDto>> GetUsersByNameAndSurname(string name, string surname);

        Task<UserDto> AuthorizeUser(UserLoginDto login);

        void RegisterUser(UserRegistrationDto user);
    }
}
