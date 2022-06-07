using CarSharingBL.DTOs;
using CarSharingBL.Facades.IFacade;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingWebAPI.Controllers
{
    [ApiController]
    public class PictureController : ControllerBase
    {
        private readonly IPictureFacade PictureFacade;

        public PictureController(IPictureFacade pictureFacade)
        {
            PictureFacade = pictureFacade;
        }

        [HttpPost("create-picture")]
        public async Task<ActionResult> CreatePicture([FromBody] PictureDto picture)
        {
            await PictureFacade.CreatePicture(picture);
            return Ok();
        }

        [HttpDelete("delete-picture/{pictureId}")]
        public async Task<ActionResult> DeletePicture(int pictureId)
        {
            await PictureFacade.DeletePicture(pictureId);
            return Ok();
        }

        [HttpGet("get-picture/{pictureId}")]
        public async Task<ActionResult<PictureDto>> GetPictureByID(int pictureId)
        {
            return Ok(await PictureFacade.GetPictureByID(pictureId));
        }

        [HttpGet("pictures")]
        public async Task<ActionResult<IEnumerable<PictureDto>>> GetAllPictures()
        {
            return Ok(await PictureFacade.GetAllPictures());
        }
    }
}
