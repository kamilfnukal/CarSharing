using CarSharingDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarSharingDAL
{
    public static class DataInit
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Picture>().HasData
                (
                new Picture() { Id = 1, Url = "urlnaobrazok1" },
                new Picture() { Id = 2, Url = "urlnaobrazok2" },
                new Picture() { Id = 3, Url = "urlnaobrazok3" }
                );

            modelBuilder.Entity<User>().HasData
                (
                new User() { Id = 1, UserName = "user1", Name = "Kamil", Surname = "Fňukal", Password = "heslo", PictureId = 1 },
                new User() { Id = 2, UserName = "patkajesaman", Name = "Patrícia", Surname = "Andicsová", Password = "bla", PictureId = 2 },
                new User() { Id = 3, UserName = "dalibor", Name = "Dalibor", Surname = "Pantlík", Password = "ahoj", PictureId = 3 }
                );

            modelBuilder.Entity<PathInfo>().HasData
            (
                new PathInfo() { Id = 1, CityName = "Brno" },
                new PathInfo() { Id = 2, CityName = "Praha" }
            );
        }
    }
}
