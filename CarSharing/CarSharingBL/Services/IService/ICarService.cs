using CarSharingBL.DTOs;
using CarSharingDAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Services.IService
{
    public interface ICarService : IBaseService<Car, CarDto>
    {
        Task<IEnumerable<CarDto>> GetCarsByUser(int userId);

        Task<IEnumerable<CarDto>> GetCarsByBrandAndNewerThan(string brand, int year);

        Task<IEnumerable<CarDto>> GetCarsWithPicture();
    }
}
