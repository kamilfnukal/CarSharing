using CarSharingBL.DTOs;
using CarSharingBL.Facades.IFacade;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingWebAPI.Controllers
{
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarFacade CarFacade;

        public CarController(ICarFacade carFacade)
        {
            CarFacade = carFacade;
        }

        [HttpPost("create-car")]
        public async Task<ActionResult> CreateCar([FromBody] CarCreateDto car)
        {
            var newCar = new CarDto()
            {
                Brand = car.Brand,
                Seats = car.Seats,
                YearOfProduction = car.YearOfProduction,
                UserId = car.UserId
            };

            await CarFacade.CreateCar(newCar);
            return Ok();
        }

        [HttpDelete("delete-car/{carId}")]
        public async Task<ActionResult> DeleteCar(int carId)
        {
            try
            {
                await CarFacade.DeleteCar(carId);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException _)
            {
                return StatusCode(10);
            }
            return Ok();
        }

        [HttpGet("get-car/{carId}")]
        public async Task<ActionResult<CarDto>> GetCarByID(int carId)
        {
            return Ok(await CarFacade.GetCarByID(carId));
        }

        [HttpGet("cars")]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetAllCars()
        {
            return Ok(await CarFacade.GetAllCars());
        }
    }
}
