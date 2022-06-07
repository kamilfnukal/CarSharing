using System.Linq;
using System.Threading.Tasks;
using CarSharingDAL;
using CarSharingDAL.Entities;
using CarSharingInfrastructure.Queries.IQuery;
using Microsoft.EntityFrameworkCore;

namespace CarSharingInfrastructure.Queries.Query
{
    public class UserQuery : BaseQuery<User>, IUserQuery
    {
        public UserQuery(CarSharingContext context) : base(context) {
            Query = Query.Include(user => user.Cars)
                         .Include(user => user.Rides);
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            return await Query.FirstOrDefaultAsync(user => user.UserName.Equals(userName));
        }

        public void FilterUsersByNameAndSurname(string name, string surname)
        {
            Query = Query.Where(user => user.Name.Equals(name) && user.Surname.Equals(surname));
        }
    }
}