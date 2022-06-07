using CarSharingBL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Facades.IFacade
{
    public interface ICarFacade
    {
        Task<int> CreateCar(CarDto car);

        Task DeleteCar(int carId);

        Task UpdateCar(CarDto car);

        Task<CarDto> GetCarByID(int carId);

        Task<IEnumerable<CarDto>> GetCarsByUser(UserDto user);

        Task<IEnumerable<CarDto>> GetCarsByBrandAndNewerThan(string brand, int year);

        Task<IEnumerable<CarDto>> GetCarsWithPicture();

        Task<IEnumerable<CarDto>> GetAllCars();
    }
}
