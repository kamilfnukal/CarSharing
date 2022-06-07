using CarSharingBL.DTOs;
using CarSharingBL.Facades.IFacade;
using CarSharingBL.Services.IService;
using CarSharingInfrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Facades.Facade
{
    public class PassengerFacade : IPassengerFacade
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IPassengerService Service;

        public PassengerFacade(IUnitOfWork unitOfWork, IPassengerService service)
        {
            UnitOfWork = unitOfWork;
            Service = service;
        }

        public async Task<int> CreatePassenger(PassengerDto passengerDto)
        {
            var passenger = Service.CreateEntity(passengerDto);
            await UnitOfWork.CommitAsync();
            return passenger.Id;
        }

        public async Task DeletePassenger(int passengerId)
        {
            Service.DeleteEntity(passengerId);
            await UnitOfWork.CommitAsync();
        }

        public async Task UpdatePassenger(PassengerDto passenger)
        {
            Service.UpdateEntity(passenger);
            await UnitOfWork.CommitAsync();
        }

        public async Task<PassengerDto> GetPassengerByID(int passengerId)
        {
            return await Service.GetEntityByID(passengerId);
        }

        public async Task<IEnumerable<PassengerDto>> GetAllPassengers()
        {
            return await Service.GetAllEntities();
        }
    }
}
