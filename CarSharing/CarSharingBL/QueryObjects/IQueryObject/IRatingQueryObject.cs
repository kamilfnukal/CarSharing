using CarSharingBL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.QueryObjects.IQueryObject
{
    public interface IRatingQueryObject
    {
        Task<IEnumerable<RatingDto>> GetRatingsWithComment();

        Task<IEnumerable<RatingDto>> GetRatingsForUser(int userId, int pageNumber, int pageSize);

        Task<IEnumerable<RatingDto>> GetBestRatingsForDriver(int userId, int howMany, int pageNumber, int pageSize);

        Task<double> GetOverallRatingForDriver(int userId);
    }
}
