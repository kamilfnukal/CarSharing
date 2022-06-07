using AutoMapper;
using CarSharingBL.DTOs;
using CarSharingBL.QueryObjects.IQueryObject;
using CarSharingInfrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.QueryObjects.QueryObject
{
    public class RideQueryObject : BaseQueryObject, IRideQueryObject
    {
        public RideQueryObject(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<IEnumerable<RideDto>> GetRidesUserDrove(int userId, int pageNumber, int pageSize)
        {
            var query = UnitOfWork.RideQuery;
            query.FilterRidesUserDrove(userId);
            query.OrderByTime(false);
            query.Page(pageSize, pageNumber);
            return Mapper.Map<IEnumerable<RideDto>>(await query.ExecuteAsync());
        }

        public async Task<IEnumerable<RideDto>> GetRidesUserWasPassenger(int userId, int pageNumber, int pageSize)
        {
            var query = UnitOfWork.RideQuery;
            query.FilterRidesUserWasPassenger(userId);
            query.OrderByTime(false);
            query.Page(pageSize, pageNumber);
            return Mapper.Map<IEnumerable<RideDto>>(await query.ExecuteAsync());
        }

        public async Task<IEnumerable<RideDto>> RidesFilteredAndOrderedById(string from, string to, DateTime date, bool ascendingOrder, int pageNumber, int pageSize)
        {
            var query = UnitOfWork.RideQuery;
            query.FilterRidesFromTo(from, to) ;
            query.FilterRidesByDate(date);
            query.FilterRidesByTime(date);
            query.FilterRidesWithFreeSeatsMoreThan(0);
            query.OrderById(ascendingOrder);
            query.Page(pageSize, pageNumber);
            return Mapper.Map<IEnumerable<RideDto>>(await query.ExecuteAsync());
        }

        public async Task<IEnumerable<RideDto>> RidesFilteredAndOrderedByPrice(string from, string to, DateTime date, bool ascendingOrder, int pageNumber, int pageSize)
        {
            var query = UnitOfWork.RideQuery;
            query.FilterRidesFromTo(from, to);
            query.FilterRidesByDate(date);
            query.FilterRidesByTime(date);
            query.FilterRidesWithFreeSeatsMoreThan(0);
            query.OrderByPrice(ascendingOrder);
            query.Page(pageSize, pageNumber);
            return Mapper.Map<IEnumerable<RideDto>>(await query.ExecuteAsync());
        }
    }
}
