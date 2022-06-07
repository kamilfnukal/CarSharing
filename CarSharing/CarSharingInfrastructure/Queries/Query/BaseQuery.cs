using CarSharingDAL;
using CarSharingDAL.Entities;
using CarSharingInfrastructure.Queries.IQuery;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSharingInfrastructure.Queries.Query
{
    public abstract class BaseQuery<TEntity> : IBaseQuery<TEntity> where TEntity : BaseEntity
    {
        protected IQueryable<TEntity> Query { get; set; }

        public BaseQuery(CarSharingContext context)
        {
            Query = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> ExecuteAsync()
        {
            return await Query?.ToListAsync() ?? new List<TEntity>();
        }

        public void Page(int pageSize, int pageNumber)
        {
            Query = Query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }
    }
}