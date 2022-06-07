using CarSharingBL.DTOs;
using CarSharingBL.Facades.IFacade;
using CarSharingBL.Services.IService;
using CarSharingInfrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Facades.Facade
{
    public class PictureFacade : IPictureFacade
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IPictureService Service;

        public PictureFacade(IUnitOfWork unitOfWork, IPictureService service)
        {
            UnitOfWork = unitOfWork;
            Service = service;
        }

        public async Task<int> CreatePicture(PictureDto pictureDto)
        {
            var picture = Service.CreateEntity(pictureDto);
            await UnitOfWork.CommitAsync();
            return picture.Id;
        }

        public async Task DeletePicture(int pictureId)
        {
            Service.DeleteEntity(pictureId);
            await UnitOfWork.CommitAsync();
        }

        public async Task UpdatePicture(PictureDto picture)
        {
            Service.UpdateEntity(picture);
            await UnitOfWork.CommitAsync();
        }

        public async Task<PictureDto> GetPictureByID(int pictureId)
        {
            return await Service.GetEntityByID(pictureId);
        }

        public async Task<IEnumerable<PictureDto>> GetAllPictures()
        {
            return await Service.GetAllEntities();
        }
    }
}
