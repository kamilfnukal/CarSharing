using AutoMapper;
using CarSharingBL.DTOs;
using CarSharingBL.QueryObjects.IQueryObject;
using CarSharingInfrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.QueryObjects.QueryObject
{
    public class UserQueryObject : BaseQueryObject, IUserQueryObject
    {
        public UserQueryObject(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<UserDto> GetUserByUserName(string userName) 
        {
            var query = UnitOfWork.UserQuery;
            var user = await query.GetUserByUserName(userName);
            return Mapper.Map<UserDto>(user);
        }

        public async Task<ICollection<UserDto>> GetUsersByNameAndSurname(string name, string surname)
        {
            var query = UnitOfWork.UserQuery;
            query.FilterUsersByNameAndSurname(name, surname);
            return Mapper.Map<ICollection<UserDto>>(await query.ExecuteAsync());
        }
    }
}
