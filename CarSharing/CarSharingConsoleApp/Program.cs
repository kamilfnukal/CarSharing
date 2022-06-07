using CarSharingDAL;
using System;
using System.Linq;

namespace CarSharingConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CarSharingContext())
            {
                
                var users = context.Users.ToList();
                Console.WriteLine("Users:");
                foreach (var user in users)
                {
                    Console.WriteLine($"{user.UserName} - {user.Name} {user.Surname}");
                }
                Console.WriteLine();

                var cities = context.Cities.ToList();
                Console.WriteLine("Cities:");
                foreach (var city in cities)
                {
                    Console.WriteLine(city.CityName);
                }
                
            }
            Console.WriteLine("ok");
            Console.ReadLine();
        }
    }
}
