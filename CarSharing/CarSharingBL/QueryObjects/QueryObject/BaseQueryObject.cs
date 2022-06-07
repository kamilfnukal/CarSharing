using AutoMapper;
using CarSharingInfrastructure.UnitOfWork;

namespace CarSharingBL.QueryObjects.QueryObject
{
    public abstract class BaseQueryObject
    {
        public readonly IUnitOfWork UnitOfWork;
        public readonly IMapper Mapper;

        public BaseQueryObject(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
    }
}
