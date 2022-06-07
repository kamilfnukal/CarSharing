using CarSharingBL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Facades.IFacade
{
    public interface IRideFacade
    {
        Task CreateRide(RideDto ride);

        Task DeleteRide(int rideId);

        Task UpdateRide(RideDto ride);

        Task<RideDto> GetRideByID(int rideId);

        Task<IEnumerable<RideDto>> RidesFilteredAndOrderedById(string from, string to, DateTime date, bool ascendingOrder, int pageNumber, int pageSize);

        Task<IEnumerable<RideDto>> RidesFilteredAndOrderedByPrice(string from, string to, DateTime date, bool ascendingOrder, int pageNumber, int pageSize);

        Task<IEnumerable<RideDto>> GetAllRides();

        Task<IEnumerable<RideDto>> GetRidesUserWasPassenger(int userId, int pageNumber, int pageSize);

        Task<IEnumerable<RideDto>> GetRidesUserDrove(int userId, int pageNumber, int pageSize);
    }
}
