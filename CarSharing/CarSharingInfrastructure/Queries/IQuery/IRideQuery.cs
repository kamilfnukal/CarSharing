using System;
using CarSharingDAL.Entities;

namespace CarSharingInfrastructure.Queries.IQuery
{
    public interface IRideQuery : IBaseQuery<Ride>
    {
        void FilterRidesWithFreeSeatsMoreThan(int numberOfFreeSeats);

        void FilterRidesUserDrove(int userId);

        void FilterRidesUserWasPassenger(int userId);

        void FilterRidesFromTo(string cityFrom, string cityTo);

        void FilterRidesByDate(DateTime date);

        void FilterRidesByTime(DateTime time);

        void OrderById(bool ascendingOrder);

        void OrderByPrice(bool ascendingOrder);

        void OrderByTime(bool ascendingOrder);
    }
}