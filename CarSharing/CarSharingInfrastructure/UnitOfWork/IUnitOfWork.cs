using CarSharingDAL.Entities;
using CarSharingInfrastructure.Queries.IQuery;
using CarSharingInfrastructure.Repository;
using System;
using System.Threading.Tasks;

namespace CarSharingInfrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Car> CarRepository { get; }

        IRepository<Passenger> PassengerRepository { get; }

        IRepository<Picture> PictureRepository { get; }

        IRepository<Rating> RatingRepository { get; }

        IRepository<Ride> RideRepository { get; }

        IRepository<User> UserRepository { get; }

        ICarQuery CarQuery { get; }

        IRatingQuery RatingQuery { get; }

        IRideQuery RideQuery { get; }

        IUserQuery UserQuery { get; }

        Task CommitAsync();
    }
}
