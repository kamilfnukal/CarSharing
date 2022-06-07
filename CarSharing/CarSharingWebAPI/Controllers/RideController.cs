using CarSharingBL.DTOs;
using CarSharingBL.Facades.IFacade;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingWebAPI.Controllers
{
    [ApiController]
    public class RideController : ControllerBase
    {
        private readonly IRideFacade RideFacade;

        public RideController(IRideFacade rideFacade)
        {
            RideFacade = rideFacade;
        }

        [HttpPost("create-ride")]
        public async Task<ActionResult> CreateRide([FromBody] RideCreateDto ride)
        {
            var newRide = new RideDto
            {
                Price = ride.Price,
                AvailableSeats = ride.AvailableSeats,
                DriverId = ride.DriverId,
                CarId = ride.CarId,
                CityFrom = ride.CityFrom.Substring(0,1).ToUpper() + ride.CityFrom[1..].ToLower(),
                CityTo = ride.CityTo.Substring(0, 1).ToUpper() + ride.CityTo[1..].ToLower(),
                DateTime = DateTime.Parse(ride.Date) + TimeSpan.Parse(ride.Time)
            };

            await RideFacade.CreateRide(newRide);
            return Ok();
        }

        [HttpDelete("delete-ride/{rideId}")]
        public async Task<ActionResult> DeleteRide(int rideId)
        {
            await RideFacade.DeleteRide(rideId);
            return Ok();
        }

        private static DateTime GetValidDatTime(string date_time)
        {          
            var dateAndTime = date_time.Split("_");
            return DateTime.ParseExact(dateAndTime[0], "dd:MM:yyyy", null) + DateTime.Parse(dateAndTime[1]).TimeOfDay;
        }

        [HttpGet("get-ride/{rideId}")]
        public async Task<ActionResult<RideDto>> GetRideByID(int rideId)
        {
            return Ok(await RideFacade.GetRideByID(rideId));
        }

        [HttpGet("/search/{cityFrom}/{cityTo}/{date_time}")]
        public async Task<ActionResult<IEnumerable<RideDto>>> RidesFilteredAndOrderedById(string cityFrom, string cityTo, string date_time, int pageNumber, int pageSize)
        {
            var dateOfRide = GetValidDatTime(date_time);
            return Ok(await RideFacade.RidesFilteredAndOrderedById(cityFrom, cityTo, dateOfRide, true, pageNumber, pageSize));
        }

        [HttpGet("/search/filter-price/{cityFrom}/{cityTo}/{date_time}")]
        public async Task<ActionResult<IEnumerable<RideDto>>> RidesFilteredAndOrderedByPrice(string cityFrom, string cityTo, string date_time, int pageNumber, int pageSize)
        {
            var dateOfRide = GetValidDatTime(date_time);
            return Ok(await RideFacade.RidesFilteredAndOrderedByPrice(cityFrom, cityTo, dateOfRide, true, pageNumber, pageSize));
        }

        [HttpGet("/search/filter-price-desc/{cityFrom}/{cityTo}/{date_time}")]
        public async Task<ActionResult<IEnumerable<RideDto>>> RidesFilteredAndOrderedByPriceDesc(string cityFrom, string cityTo, string date_time, int pageNumber, int pageSize)
        {
            var dateOfRide = GetValidDatTime(date_time);
            return Ok(await RideFacade.RidesFilteredAndOrderedByPrice(cityFrom, cityTo, dateOfRide, false, pageNumber, pageSize));
        }

        [HttpGet("/passenger/{userId}")]
        public async Task<ActionResult<IEnumerable<RideDto>>> RidesUserWasPassanger(int userId, int pageNumber, int pageSize)
        {
            return Ok(await RideFacade.GetRidesUserWasPassenger(userId, pageNumber, pageSize));
        }

        [HttpGet("/driver/{userId}")]
        public async Task<ActionResult<IEnumerable<RideDto>>> RidesUserWasDriver(int userId, int pageNumber, int pageSize)
        {
            return Ok(await RideFacade.GetRidesUserDrove(userId, pageNumber, pageSize));
        }

        [HttpGet("rides")]
        public async Task<ActionResult<IEnumerable<RideDto>>> GetAllRides()
        {
            return Ok(await RideFacade.GetAllRides());
        }
    }
}
