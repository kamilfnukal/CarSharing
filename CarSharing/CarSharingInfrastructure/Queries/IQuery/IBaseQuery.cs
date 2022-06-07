using CarSharingDAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingInfrastructure.Queries.IQuery
{
    public interface IBaseQuery<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> ExecuteAsync();

        void Page(int pageSize, int pageNumber);
    }
}
