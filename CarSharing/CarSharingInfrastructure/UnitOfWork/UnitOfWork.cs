using CarSharingDAL;
using CarSharingDAL.Entities;
using CarSharingInfrastructure.Queries.IQuery;
using CarSharingInfrastructure.Repository;
using System;
using System.Threading.Tasks;

namespace CarSharingInfrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private CarSharingContext Context;

        private bool Disposed = false;

        public IRepository<Car> CarRepository { get; }

        public IRepository<Passenger> PassengerRepository { get; }

        public IRepository<Picture> PictureRepository { get; }

        public IRepository<Rating> RatingRepository { get; }

        public IRepository<Ride> RideRepository { get; }

        public IRepository<User> UserRepository { get; }

        public ICarQuery CarQuery { get; }

        public IRatingQuery RatingQuery { get; }

        public IRideQuery RideQuery { get;}

        public IUserQuery UserQuery { get;}

        public UnitOfWork(CarSharingContext carSharingContext, IRepository<Car> carRepository, IRepository<Passenger> passengerRepository, IRepository<Picture> pictureRepository,
                          IRepository<Rating> ratingRepository, IRepository<Ride> rideRepository, IRepository<User> userRepository, ICarQuery carQuery, IRatingQuery ratingQuery,
                          IRideQuery rideQuery, IUserQuery userQuery)
        {
            Context = carSharingContext;
            CarRepository = carRepository;
            PassengerRepository = passengerRepository;
            PictureRepository = pictureRepository;
            RatingRepository = ratingRepository;
            RideRepository = rideRepository;
            UserRepository = userRepository;

            CarQuery = carQuery;
            RatingQuery = ratingQuery;
            RideQuery = rideQuery;
            UserQuery = userQuery;
        }

        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed && disposing)
            {
                Context.Dispose();
            }
            Disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
