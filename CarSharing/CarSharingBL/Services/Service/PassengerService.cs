using AutoMapper;
using CarSharingBL.DTOs;
using CarSharingBL.Services.IService;
using CarSharingDAL.Entities;
using CarSharingInfrastructure.Repository;

namespace CarSharingBL.Services.Service
{
    public class PassengerService : BaseService<Passenger, PassengerDto>, IPassengerService
    {
        public PassengerService(IRepository<Passenger> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
