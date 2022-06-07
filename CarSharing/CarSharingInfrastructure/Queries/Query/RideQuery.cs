using System;
using System.Linq;
using CarSharingDAL;
using CarSharingDAL.Entities;
using CarSharingInfrastructure.Queries.IQuery;
using Microsoft.EntityFrameworkCore;

namespace CarSharingInfrastructure.Queries.Query
{
    public class RideQuery : BaseQuery<Ride>, IRideQuery
    {
        public RideQuery(CarSharingContext context) : base(context) 
        {
            Query = Query.Include(ride => ride.Ratings)
                         .Include(ride => ride.Passengers);
        }

        public void FilterRidesWithFreeSeatsMoreThan(int numberOfFreeSeats)
        {
            Query = Query.Where(ride => ride.AvailableSeats > numberOfFreeSeats);
        }

        public void FilterRidesUserDrove(int userId)
        {
            Query = Query.Where(ride => ride.DriverId == userId);
        }

        public void FilterRidesUserWasPassenger(int userId)
        {
            Query = Query.Where(ride => ride.Passengers.Any(passenger => passenger.UserId == userId));
        }

        public void FilterRidesFromTo(string cityFrom, string cityTo)
        {
            Query = Query.Where(ride => ride.CityFrom.Equals(cityFrom) && ride.CityTo.Equals(cityTo));
        }

        public void FilterRidesByDate(DateTime date)
        {
            Query = Query.Where(ride => ride.DateTime.Date == date.Date);
        }

        public void FilterRidesByTime(DateTime time)
        {
            var hourLess = time.AddHours(-4);
            var hourMore = time.AddHours(4);
            Query = Query.Where(ride => ride.DateTime >= hourLess && ride.DateTime <= hourMore);
        }

        public void OrderById(bool ascendingOrder)
        {
            Query = ascendingOrder ? Query.OrderBy(ride => ride.Id) : Query.OrderByDescending(ride => ride.Id);
        }

        public void OrderByPrice(bool ascendingOrder)
        {
            Query = ascendingOrder ? Query.OrderBy(ride => ride.Price) : Query.OrderByDescending(ride => ride.Price);
        }

        public void OrderByTime(bool ascendingOrder)
        {
            Query = ascendingOrder ? Query.OrderBy(ride => ride.DateTime) : Query.OrderByDescending(ride => ride.DateTime);
        }
    }
}