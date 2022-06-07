using CarSharingBL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.QueryObjects.IQueryObject
{
    public interface IRideQueryObject
    {
        Task<IEnumerable<RideDto>> GetRidesUserDrove(int userId, int pageNumber, int pageSize);

        Task<IEnumerable<RideDto>> GetRidesUserWasPassenger(int userId, int pageNumber, int pageSize);

        Task<IEnumerable<RideDto>> RidesFilteredAndOrderedById(string from, string to, DateTime date, bool ascendingOrder, int pageNumber, int pageSize);

        Task<IEnumerable<RideDto>> RidesFilteredAndOrderedByPrice(string from, string to, DateTime date, bool ascendingOrder, int pageNumber, int pageSize);
    }
}
