using CarSharingDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarSharingDAL
{
    public static class DataInit
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData
                (
                new User() { Id = 1, UserName = "user1", Name = "Kamil", Surname = "Fňukal" },
                new User() { Id = 2, UserName = "patkajesaman", Name = "Patrícia", Surname = "Andicsová" },
                new User() { Id = 3, UserName = "dalibor", Name = "Dalibor", Surname = "Pantlík" }
                ) ;
        }
    }
}
