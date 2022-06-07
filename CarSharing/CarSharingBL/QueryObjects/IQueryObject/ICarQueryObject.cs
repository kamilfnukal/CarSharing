using CarSharingBL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.QueryObjects.IQueryObject
{
    public interface ICarQueryObject
    {
        Task<IEnumerable<CarDto>> GetCarsByUser(int userId);

        Task<IEnumerable<CarDto>> GetCarsByBrandAndNewerThan(string brand, int year);

        Task<IEnumerable<CarDto>> GetCarsWithPicture();
    }
}
