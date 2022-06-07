using CarSharingDAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingInfrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllEntities();

        Task<TEntity> GetEntityById(int entityId);

        TEntity CreateEntity(TEntity entity);

        void DeleteEntity(int entityId);

        void UpdateEntity(TEntity entity);

        void Save();
    }
}
