using CarSharingBL.DTOs;
using CarSharingDAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Services.IService
{
    public interface IRideService : IBaseService<Ride, RideDto>
    {
        Task<IEnumerable<RideDto>> RidesFilteredAndOrderedById(string from, string to, DateTime date, bool ascendingOrder, int pageNumber, int pageSize);

        Task<IEnumerable<RideDto>> RidesFilteredAndOrderedByPrice(string from, string to, DateTime date, bool ascendingOrder, int pageNumber, int pageSize);

        Task<IEnumerable<RideDto>> GetRidesUserWasPassenger(int userId, int pageNumber, int pageSize);

        Task<IEnumerable<RideDto>> GetRidesUserDrove(int userId, int pageNumber, int pageSize);
    }
}
