using CarSharingBL.DTOs;
using CarSharingBL.Facades.IFacade;
using CarSharingBL.Services.IService;
using CarSharingInfrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Facades.Facade
{
    public class RideFacade : IRideFacade
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IRideService Service;

        public RideFacade(IUnitOfWork unitOfWork, IRideService service)
        {
            UnitOfWork = unitOfWork;
            Service = service;
        }

        public async Task CreateRide(RideDto ride)
        {
            Service.CreateEntity(ride);
            await UnitOfWork.CommitAsync();
        }

        public async Task DeleteRide(int rideId)
        {
            Service.DeleteEntity(rideId);
            await UnitOfWork.CommitAsync();
        }

        public async Task UpdateRide(RideDto ride)
        {
            Service.UpdateEntity(ride);
            await UnitOfWork.CommitAsync();
        }

        public async Task<RideDto> GetRideByID(int rideId)
        {
            return await Service.GetEntityByID(rideId);
        }

        public async Task<IEnumerable<RideDto>> RidesFilteredAndOrderedById(string from, string to, DateTime date, bool ascendingOrder, int pageNumber, int pageSize)
        {
            return await Service.RidesFilteredAndOrderedById(from, to, date, ascendingOrder, pageNumber, pageSize);
        }

        public async Task<IEnumerable<RideDto>> RidesFilteredAndOrderedByPrice(string from, string to, DateTime date, bool ascendingOrder, int pageNumber, int pageSize)
        {
            return await Service.RidesFilteredAndOrderedByPrice(from, to, date, ascendingOrder, pageNumber, pageSize);
        }

        public async Task<IEnumerable<RideDto>> GetAllRides()
        {
            return await Service.GetAllEntities();
        }

        public async Task<IEnumerable<RideDto>> GetRidesUserWasPassenger(int userId, int pageNumber, int pageSize)
        {
            return await Service.GetRidesUserWasPassenger(userId, pageNumber, pageSize);
        }

        public async Task<IEnumerable<RideDto>> GetRidesUserDrove(int userId, int pageNumber, int pageSize)
        {
            return await Service.GetRidesUserDrove(userId, pageNumber, pageSize);
        }
    }
}
