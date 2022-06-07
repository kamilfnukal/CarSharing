using AutoMapper;
using CarSharingBL.DTOs;
using CarSharingBL.QueryObjects.IQueryObject;
using CarSharingInfrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.QueryObjects.QueryObject
{
    public class RatingQueryObject : BaseQueryObject, IRatingQueryObject
    {
        public RatingQueryObject(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<IEnumerable<RatingDto>> GetRatingsWithComment()
        {
            var query = UnitOfWork.RatingQuery;
            query.FilterRatingsWithComment();
            return Mapper.Map<IEnumerable<RatingDto>>(await query.ExecuteAsync());
        }

        public async Task<IEnumerable<RatingDto>> GetRatingsForUser(int userId, int pageNumber, int pageSize)
        {
            var query = UnitOfWork.RatingQuery;
            query.FilterRatingsForUser(userId);
            query.Page(pageSize, pageNumber);
            return Mapper.Map<IEnumerable<RatingDto>>(await query.ExecuteAsync());
        }

        public async Task<IEnumerable<RatingDto>> GetBestRatingsForDriver(int userId, int howMany, int pageNumber, int pageSize)
        {
            var query = UnitOfWork.RatingQuery;
            query.FilterRatingsForUser(userId);
            query.OrderByRate(false);
            query.Take(howMany);
            query.Page(pageSize, pageNumber);
            return Mapper.Map<IEnumerable<RatingDto>>(await query.ExecuteAsync());
        }

        public async Task<double> GetOverallRatingForDriver(int userId)
        {
            var query = UnitOfWork.RatingQuery;
            query.FilterRatingsForUser(userId);
            var totalCount = await query.GetTotalCount();
            var sumOfRates = await query.SumOfRates();
            return sumOfRates / totalCount;
        }
    }
}
