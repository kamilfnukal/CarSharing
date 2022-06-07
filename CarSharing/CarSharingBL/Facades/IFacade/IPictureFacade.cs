using CarSharingBL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Facades.IFacade
{
    public interface IPictureFacade
    {
        Task<int> CreatePicture(PictureDto pictureDto);

        Task DeletePicture(int pictureId);

        Task UpdatePicture(PictureDto picture);

        Task<PictureDto> GetPictureByID(int pictureId);

        Task<IEnumerable<PictureDto>> GetAllPictures();

    }
}
