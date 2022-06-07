using CarSharingBL.DTOs;
using CarSharingBL.Facades.IFacade;
using CarSharingBL.Services.IService;
using CarSharingInfrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Facades.Facade
{
    public class CarFacade : ICarFacade
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly ICarService Service;

        public CarFacade(IUnitOfWork unitOfWork, ICarService service)
        {
            UnitOfWork = unitOfWork;
            Service = service;
        }

        public async Task<int> CreateCar(CarDto car)
        {
            var ent = Service.CreateEntity(car);
            await UnitOfWork.CommitAsync();
            return ent.Id;
        }

        public async Task DeleteCar(int carId)
        {
            Service.DeleteEntity(carId);
            await UnitOfWork.CommitAsync();
        }

        public async Task UpdateCar(CarDto car)
        {
            Service.UpdateEntity(car);
            await UnitOfWork.CommitAsync();          
        }

        public async Task<CarDto> GetCarByID(int carId)
        {
            return await Service.GetEntityByID(carId);
        }

        public async Task<IEnumerable<CarDto>> GetCarsByUser(UserDto user)
        {
            return await Service.GetCarsByUser(user.Id);
        }

        public async Task<IEnumerable<CarDto>> GetCarsByBrandAndNewerThan(string brand, int year)
        {
            return await Service.GetCarsByBrandAndNewerThan(brand, year);
        }

        public async Task<IEnumerable<CarDto>> GetCarsWithPicture()
        {
            return await Service.GetCarsWithPicture();
        }

        public async Task<IEnumerable<CarDto>> GetAllCars()
        {
            return await Service.GetAllEntities();
        }
    }
}
