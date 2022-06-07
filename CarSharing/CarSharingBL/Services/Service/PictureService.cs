using AutoMapper;
using CarSharingBL.DTOs;
using CarSharingBL.Services.IService;
using CarSharingDAL.Entities;
using CarSharingInfrastructure.Repository;

namespace CarSharingBL.Services.Service
{
    public class PictureService : BaseService<Picture, PictureDto>, IPictureService
    {
        public PictureService(IRepository<Picture> repository, IMapper mapper) : base(repository, mapper) { }
    }
}