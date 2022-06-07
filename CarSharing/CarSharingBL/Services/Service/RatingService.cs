using AutoMapper;
using CarSharingBL.DTOs;
using CarSharingBL.QueryObjects.IQueryObject;
using CarSharingBL.Services.IService;
using CarSharingDAL.Entities;
using CarSharingInfrastructure.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Services.Service
{
    public class RatingService : BaseService<Rating, RatingDto>, IRatingService
    {
        private readonly IRatingQueryObject QueryObject;

        public RatingService(IRepository<Rating> repository, IMapper mapper, IRatingQueryObject ratingQueryObject)
            : base(repository, mapper)
        {
            QueryObject = ratingQueryObject;
        }

        public async Task<IEnumerable<RatingDto>> GetRatingsWithComment()
        {
            return await QueryObject.GetRatingsWithComment();   
        }

        public async Task<IEnumerable<RatingDto>> GetRatingsForUser(int userId, int pageNumber, int pageSize)
        {
            return await QueryObject.GetRatingsForUser(userId, pageNumber, pageSize);
        }

        public async Task<IEnumerable<RatingDto>> GetBestRatingsForDriver(int userId, int howMany, int pageNumber, int pageSize)
        {
            return await QueryObject.GetBestRatingsForDriver(userId, howMany, pageNumber, pageSize);
        }

        public async Task<double> GetOverallRatingForDriver(int userId)
        {
            return await QueryObject.GetOverallRatingForDriver(userId);
        }
    }
}
