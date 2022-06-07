using AutoMapper;
using CarSharingBL.DTOs;
using CarSharingBL.QueryObjects.IQueryObject;
using CarSharingInfrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.QueryObjects.QueryObject
{
    public class CarQueryObject : BaseQueryObject, ICarQueryObject
    {
        public CarQueryObject(IUnitOfWork unitOfWork, IMapper mapper) : base (unitOfWork, mapper) { }

        public async Task<IEnumerable<CarDto>> GetCarsByUser(int userId)
        {
            var query = UnitOfWork.CarQuery;
            query.FilterCarsByUser(userId);
            return Mapper.Map<IEnumerable<CarDto>>(await query.ExecuteAsync());
        }

        public async Task<IEnumerable<CarDto>> GetCarsByBrandAndNewerThan(string brand, int year)
        {
            var query = UnitOfWork.CarQuery;
            query.FilterCarsByBrand(brand);
            query.FilterCarsNewerThan(year);
            return Mapper.Map<IEnumerable<CarDto>>(await query.ExecuteAsync());
        }

        public async Task<IEnumerable<CarDto>> GetCarsWithPicture()
        {
            var query = UnitOfWork.CarQuery;
            query.FilterCarsWithPicture();
            return Mapper.Map<IEnumerable<CarDto>>(await query.ExecuteAsync());
        }
    }
}
