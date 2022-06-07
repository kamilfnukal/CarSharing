using CarSharingBL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Facades.IFacade
{
    public interface IUserFacade
    {
        Task CreateUser(UserDto user);

        Task DeleteUser(int userId);

        Task UpdateUser(UserDto user);

        Task<UserDto> GetUserByID(int userId);

        Task<DriverDto> GetDriverById(int driverId);

        Task<bool> UserExists(string userName);

        Task<UserDto> Login(UserLoginDto userLogin);

        Task RegisterUser(UserRegistrationDto user);

        Task<UserDto> GetUserByUserName(string userName);

        Task<ICollection<UserDto>> GetUsersByNameAndSurname(string name, string surname);

        Task<IEnumerable<UserDto>> GetAllUsers();
    }
}
