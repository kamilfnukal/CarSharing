using CarSharingBL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Facades.IFacade
{
    public interface IRatingFacade
    {
        Task CreateRating(RatingDto rating);

        Task DeleteRating(int ratingId);

        Task UpdateRating(RatingDto rating);

        Task<RatingDto> GetRatingByID(int ratingId);

        Task<IEnumerable<RatingDto>> GetRatingsWithComment();

        Task<IEnumerable<RatingDto>> GetRatingsForUser(int userId, int pageNumber, int pageSize);

        Task<IEnumerable<RatingDto>> GetBestRatingsForDriver(UserDto user, int howMany, int pageNumber, int pageSize);

        Task<double> GetOverallRatingForDriver(UserDto user);

        Task<IEnumerable<RatingDto>> GetAllRatings();
    }
}
