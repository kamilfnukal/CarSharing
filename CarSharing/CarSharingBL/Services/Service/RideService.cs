using AutoMapper;
using CarSharingBL.DTOs;
using CarSharingBL.QueryObjects.IQueryObject;
using CarSharingBL.Services.IService;
using CarSharingDAL.Entities;
using CarSharingInfrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Services.Service
{
    public class RideService : BaseService<Ride, RideDto>, IRideService
    {
        private readonly IRideQueryObject QueryObject;

        public RideService(IRepository<Ride> repository, IMapper mapper, IRideQueryObject rideQueryObject)
            : base(repository, mapper)
        {
            QueryObject = rideQueryObject;
        }

        public async Task<IEnumerable<RideDto>> RidesFilteredAndOrderedById(string from, string to, DateTime date, bool ascendingOrder, int pageNumber, int pageSize)
        {
            return (from != null && to != null) ? await QueryObject.RidesFilteredAndOrderedById(from, to, date, ascendingOrder, pageNumber, pageSize) : null;
        }

        public async Task<IEnumerable<RideDto>> RidesFilteredAndOrderedByPrice(string from, string to, DateTime date, bool ascendingOrder, int pageNumber, int pageSize)
        {
            return (from != null && to != null) ? await QueryObject.RidesFilteredAndOrderedByPrice(from, to, date, ascendingOrder, pageNumber, pageSize) : null;
        }

        public async Task<IEnumerable<RideDto>> GetRidesUserWasPassenger(int userId, int pageNumber, int pageSize)
        {
            return await QueryObject.GetRidesUserWasPassenger(userId, pageNumber, pageSize);
        }

        public async Task<IEnumerable<RideDto>> GetRidesUserDrove(int userId, int pageNumber, int pageSize)
        {
            return await QueryObject.GetRidesUserDrove(userId, pageNumber, pageSize);
        }


    }
}
