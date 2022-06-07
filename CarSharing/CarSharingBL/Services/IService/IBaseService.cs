using CarSharingBL.DTOs;
using CarSharingDAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingBL.Services.IService
{
    public interface IBaseService<TEntity, TDto> where TEntity : BaseEntity
                                                 where TDto : BaseDto
    {
        Task<IEnumerable<TDto>> GetAllEntities();

        Task<TDto> GetEntityByID(int entityId);

        TEntity CreateEntity(TDto entityDto);

        void DeleteEntity(int entityId);

        void UpdateEntity(TDto entityDto);
    }
}
