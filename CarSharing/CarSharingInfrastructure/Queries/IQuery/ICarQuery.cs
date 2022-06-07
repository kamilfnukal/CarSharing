using System.Threading.Tasks;
using CarSharingDAL.Entities;

namespace CarSharingInfrastructure.Queries.IQuery
{
    public interface ICarQuery : IBaseQuery<Car>
    {
        void FilterCarsByUser(int userId);

        void FilterCarsByBrand(string brand);

        void FilterCarsNewerThan(int year);

        void FilterCarsWithPicture();

        Task<int> GetLastCarId();
    }
}