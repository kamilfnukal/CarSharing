using CarSharingBL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.QueryObjects.IQueryObject
{
    public interface IUserQueryObject
    { 
        Task<UserDto> GetUserByUserName(string userName);

        Task<ICollection<UserDto>> GetUsersByNameAndSurname(string name, string surname);
    }
}
