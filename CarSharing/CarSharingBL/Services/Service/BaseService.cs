using AutoMapper;
using CarSharingBL.DTOs;
using CarSharingBL.Services.IService;
using CarSharingDAL.Entities;
using CarSharingInfrastructure.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Services.Service
{
    public abstract class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto> where TEntity : BaseEntity
                                                                                   where TDto : BaseDto
    {
        internal readonly IRepository<TEntity> Repository;
        internal readonly IMapper Mapper;

        protected BaseService(IRepository<TEntity> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> GetAllEntities()
        {
            return Mapper.Map<IEnumerable<TDto>>(await Repository.GetAllEntities());
        }

        public async Task<TDto> GetEntityByID(int entityId)
        {
            var entity = await Repository.GetEntityById(entityId);
            return Mapper.Map<TDto>(entity);
        }

        public TEntity CreateEntity(TDto entityDto)
        {
            var entity = Mapper.Map<TEntity>(entityDto);
            return Repository.CreateEntity(entity);
        }

        public void DeleteEntity(int entityId)
        {
            Repository.DeleteEntity(entityId);
        }

        public void UpdateEntity(TDto entityDto)
        {
            var entity = Mapper.Map<TEntity>(entityDto);
            Repository.UpdateEntity(Mapper.Map<TEntity>(entity));
        }
    }
}
