using AutoMapper;
using CarSharingBL.DTOs;
using CarSharingBL.QueryObjects.IQueryObject;
using CarSharingBL.Services.IService;
using CarSharingDAL.Entities;
using CarSharingInfrastructure.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Services.Service
{
    public class CarService : BaseService<Car, CarDto>, ICarService
    {
        private readonly ICarQueryObject QueryObject;

        public CarService(IRepository<Car> repository, IMapper mapper, ICarQueryObject carQueryObject)
            : base(repository, mapper) 
        {
            QueryObject = carQueryObject;
        }

        public async Task<IEnumerable<CarDto>> GetCarsByUser(int userId)
        {
            return await QueryObject.GetCarsByUser(userId);
        }

        public async Task<IEnumerable<CarDto>> GetCarsByBrandAndNewerThan(string brand, int year)
        {
            if (brand == null) 
            {
                brand = string.Empty;
            }
            return await QueryObject.GetCarsByBrandAndNewerThan(brand, year);
        }

        public async Task<IEnumerable<CarDto>> GetCarsWithPicture()
        {
            return await QueryObject.GetCarsWithPicture();
        }
    }
}
