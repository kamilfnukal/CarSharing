using System.Threading.Tasks;
using CarSharingDAL.Entities;

namespace CarSharingInfrastructure.Queries.IQuery
{
    public interface IRatingQuery : IBaseQuery<Rating>
    {
        void FilterRatingsWithComment();

        void FilterRatingsForUser(int userId);

        void Take(int howMany);

        Task<int> SumOfRates();

        Task<int> GetTotalCount();

        void OrderByRate(bool ascendingOrder);
    }
}