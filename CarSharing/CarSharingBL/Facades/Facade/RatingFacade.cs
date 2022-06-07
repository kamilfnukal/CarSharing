using CarSharingBL.DTOs;
using CarSharingBL.Facades.IFacade;
using CarSharingBL.Services.IService;
using CarSharingInfrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Facades.Facade
{
    public class RatingFacade : IRatingFacade
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IRatingService Service;

        public RatingFacade(IUnitOfWork unitOfWork, IRatingService service)
        {
            UnitOfWork = unitOfWork;
            Service = service;
        }

        public async Task CreateRating(RatingDto rating)
        {
            Service.CreateEntity(rating);
            await UnitOfWork.CommitAsync();
        }

        public async Task DeleteRating(int ratingId)
        {
            Service.DeleteEntity(ratingId);
            await UnitOfWork.CommitAsync();
        }

        public async Task UpdateRating(RatingDto rating)
        {
            Service.UpdateEntity(rating);
            await UnitOfWork.CommitAsync();
        }

        public async Task<RatingDto> GetRatingByID(int ratingId)
        {
            return await Service.GetEntityByID(ratingId);
        }

        public async Task<IEnumerable<RatingDto>> GetRatingsWithComment()
        {
            return await Service.GetRatingsWithComment();
        }

        public async Task<IEnumerable<RatingDto>> GetRatingsForUser(int userId, int pageNumber, int pageSize)
        {
            return await Service.GetRatingsForUser(userId, pageNumber, pageSize);
        }

        public async Task<IEnumerable<RatingDto>> GetBestRatingsForDriver(UserDto user, int howMany, int pageNumber, int pageSize)
        {
            return await Service.GetBestRatingsForDriver(user.Id, howMany, pageNumber, pageSize);
        }

        public async Task<double> GetOverallRatingForDriver(UserDto user)
        {
            return await Service.GetOverallRatingForDriver(user.Id);
        }

        public async Task<IEnumerable<RatingDto>> GetAllRatings()
        {
            return await Service.GetAllEntities();
        }
    }
}
