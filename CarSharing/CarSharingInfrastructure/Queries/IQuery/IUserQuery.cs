using System.Threading.Tasks;
using CarSharingDAL.Entities;

namespace CarSharingInfrastructure.Queries.IQuery
{
    public interface IUserQuery : IBaseQuery<User>
    {
        Task<User> GetUserByUserName(string userName);

        void FilterUsersByNameAndSurname(string name, string surname);
    }
}