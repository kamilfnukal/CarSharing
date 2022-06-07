using System.Linq;
using System.Threading.Tasks;
using CarSharingDAL;
using CarSharingDAL.Entities;
using CarSharingInfrastructure.Queries.IQuery;
using Microsoft.EntityFrameworkCore;

namespace CarSharingInfrastructure.Queries.Query
{
    public class RatingQuery : BaseQuery<Rating>, IRatingQuery
    {
        public RatingQuery(CarSharingContext context) : base(context) 
        {
            Query = Query.Include(rating => rating.ForUser);
        }

        public void FilterRatingsWithComment()
        {
            Query = Query.Where(rating => rating.Comment != "");
        }

        public void FilterRatingsForUser(int userId) 
        {
            Query = Query.Where(rating => rating.ForUserId == userId);
        }

        public void Take(int howMany)
        {
            Query = Query.Take(howMany);
        }

        public async Task<int> SumOfRates()
        {
            return await Query.Select(rating => rating.Rate).SumAsync();
        }

        public async Task<int> GetTotalCount()
        {
            return await Query.CountAsync();
        }

        public void OrderByRate(bool ascendingOrder)
        {
            Query = ascendingOrder ? Query.OrderBy(rating => rating.Rate) : Query.OrderByDescending(ride => ride.Id);
        }
    }
}