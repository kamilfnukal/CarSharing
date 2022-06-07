using CarSharingBL.DTOs;
using CarSharingDAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Services.IService
{
    public interface IRatingService : IBaseService<Rating, RatingDto>
    {
        Task<IEnumerable<RatingDto>> GetRatingsWithComment();

        Task<IEnumerable<RatingDto>> GetRatingsForUser(int userId, int pageNumber, int pageSize);

        Task<IEnumerable<RatingDto>> GetBestRatingsForDriver(int userId, int howMany, int pageNumber, int pageSize);

        Task<double> GetOverallRatingForDriver(int userId);
    }
}
