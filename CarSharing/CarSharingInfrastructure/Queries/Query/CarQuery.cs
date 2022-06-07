using System.Linq;
using System.Threading.Tasks;
using CarSharingDAL;
using CarSharingDAL.Entities;
using CarSharingInfrastructure.Queries.IQuery;
using Microsoft.EntityFrameworkCore;

namespace CarSharingInfrastructure.Queries.Query
{
    public class CarQuery : BaseQuery<Car>, ICarQuery
    {
        public CarQuery(CarSharingContext query) : base(query)
        {
            Query = Query.Include(car => car.Pictures);
        }

        public void FilterCarsByUser(int userId)
        {
            Query = Query.Where(car => car.UserId == userId);
        }

        public void FilterCarsByBrand(string brand)
        {
            Query = Query.Where(car => car.Brand == brand);
        }

        public void FilterCarsNewerThan(int year)
        {
            Query = Query.Where(car => car.YearOfProduction >= year);
        }

        public void FilterCarsWithPicture()
        {
            Query = Query.Where(car => car.Pictures.Count != 0);
        }

        public async Task<int> GetLastCarId()
        {
            return await Query.MaxAsync(car => car.Id);
        }
    }
}