using CarSharingBL.DTOs;
using CarSharingBL.Facades.IFacade;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSharingWebAPI.Controllers
{
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly IPassengerFacade PassengerFacade;
        private readonly IRideFacade RideFacade;

        public PassengerController(IPassengerFacade passengerFacade, IRideFacade rideFacade)
        {
            PassengerFacade = passengerFacade;
            RideFacade = rideFacade;
        }

        [HttpPost("create-passenger")]
        public async Task<ActionResult> CreatePassenger([FromBody] PassengerCreateDto passenger)
        {
            var newPassenger = new PassengerDto
            {
                UserId = passenger.UserId,
                RideId = passenger.RideId
            };

            var ride = await RideFacade.GetRideByID(passenger.RideId);
            
            if(ride != null && ride.AvailableSeats > 0 && ride.DriverId != passenger.UserId && !ride.Passengers.Any(u => u.Id == passenger.UserId) )
            {
                await PassengerFacade.CreatePassenger(newPassenger);
                ride.AvailableSeats--;
                await RideFacade.UpdateRide(ride);
                return Ok();
            }
            return StatusCode(10);
        }

        [HttpDelete("delete-passenger/{rideId}/{passId}")]
        public async Task<ActionResult> DeletePassenger(int rideId, int passId)
        {
            var ride = await RideFacade.GetRideByID(rideId);
            
            if (ride.Passengers.Any(passanger => passanger.Id == passId))
            {
                ride.AvailableSeats++;
                await PassengerFacade.DeletePassenger(passId);
                await RideFacade.UpdateRide(ride);
                return Ok();
            }
            return StatusCode(10);
        }

        [HttpGet("get-passenger/{passengerId}")]
        public async Task<ActionResult<PassengerDto>> GetPassengerByID(int passengerId)
        {
            return Ok(await PassengerFacade.GetPassengerByID(passengerId));
        }

        [HttpGet("passengers")]
        public async Task<ActionResult<IEnumerable<PassengerDto>>> GetAllPassengers()
        {
            return Ok(await PassengerFacade.GetAllPassengers());
        }
    }
}
