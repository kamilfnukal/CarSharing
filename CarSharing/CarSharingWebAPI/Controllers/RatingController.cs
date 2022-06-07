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
    public class RatingController : ControllerBase
    {
        private readonly IRatingFacade RatingFacade;

        public RatingController(IRatingFacade ratingFacade)
        {
            RatingFacade = ratingFacade;
        }

        [HttpPost("create-rating")]
        public async Task<ActionResult> CreateRating([FromBody] RatingDto rating)
        {
            await RatingFacade.CreateRating(rating);
            return Ok();
        }

        [HttpDelete("delete-rating/{ratingId}")]
        public async Task<ActionResult> DeleteRating(int ratingId)
        {
            await RatingFacade.DeleteRating(ratingId);
            return Ok();
        }

        [HttpGet("get-rating/{ratingId}")]
        public async Task<ActionResult<RatingDto>> GetRatingByID(int ratingId)
        {
            return Ok(await RatingFacade.GetRatingByID(ratingId));
        }

        [HttpGet("ratings/{userId}")]
        public async Task<ActionResult<IEnumerable<RatingDto>>> GetRatingsForUser(int userId, int pageNumber, int pageSize)
        {
            return Ok(await RatingFacade.GetRatingsForUser(userId, pageNumber, pageSize));
        }

        [HttpGet("ratings")]
        public async Task<ActionResult<IEnumerable<RatingDto>>> GetAllRatings()
        {
            return Ok(await RatingFacade.GetAllRatings());
        }
    }
}
