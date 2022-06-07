using CarSharingDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarSharingDAL
{
    public class CarSharingContext : DbContext
    {
        private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Integrated Security=True;MultipleActiveResultSets=True;Database=CarSharing;Trusted_Connection=True;";
        // private const string ConnectionString = "Server=localhost,1433; Database=CarSharingDB1;User=sa; Password=yourStrong(!)Password";
        public DbSet<Car> Cars { get; set; }
        public DbSet<PathInfo> Cities { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Passenger> Passengers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(ConnectionString)
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
