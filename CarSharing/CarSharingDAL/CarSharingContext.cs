using CarSharingDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CarSharingDAL
{
    public class CarSharingContext : DbContext
    {
        private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Integrated Security=True;MultipleActiveResultSets=True;Database=CarSharing;Trusted_Connection=True;";
        // private const string ConnectionString = "Server=localhost;Port=5432;Database=postgres13;User Id=postgres;Password=postgres"; 

        public DbSet<Car> Cars { get; set; }
       
        public DbSet<Passenger> Passengers { get; set; }

        public DbSet<Picture> Pictures { get; set; }
        
        public DbSet<Rating> Ratings { get; set; }
        
        public DbSet<Ride> Rides { get; set; }
       
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder
                .UseSqlServer(ConnectionString)
                .EnableSensitiveDataLogging()
                .UseLazyLoadingProxies();
            /*
            optionsBuilder
                .UseNpgsql(ConnectionString)
                .EnableSensitiveDataLogging()
                .UseLazyLoadingProxies();
            */
               
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            
            modelBuilder.Entity<User>()
                .HasMany<Ride>(user => user.Rides)
                .WithOne(ride => ride.Driver)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany<Car>(user => user.Cars)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Car>()
                .HasMany<Picture>(car => car.Pictures)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ride>()
                .HasMany<Rating>(ride => ride.Ratings)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ride>()
                .HasMany<Passenger>(ride => ride.Passengers)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
