using CarSharingBL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarSharingBL.Facades.IFacade
{
    public interface IPassengerFacade
    {
        Task<int> CreatePassenger(PassengerDto passengerDto);

        Task DeletePassenger(int passengerId);

        Task UpdatePassenger(PassengerDto passenger);

        Task<PassengerDto> GetPassengerByID(int passengerId);

        Task<IEnumerable<PassengerDto>> GetAllPassengers();
    }
}
