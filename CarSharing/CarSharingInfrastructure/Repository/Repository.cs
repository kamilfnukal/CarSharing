using CarSharingDAL;
using CarSharingDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSharingInfrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        public CarSharingContext Context { get; set; }

        public bool Disposed { get; set; }

        public Repository(CarSharingContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllEntities()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetEntityById(int entityId)
        {
            return await Context.Set<TEntity>().FindAsync(entityId);
        }

        public TEntity CreateEntity(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return entity;
        }

        public void DeleteEntity(int entityId)
        {
            var entity = Context.Set<TEntity>().Find(entityId);
            Context.Set<TEntity>().Remove(entity);
        }

        public void UpdateEntity(TEntity entity)
        {
            var foundEntity = Context.Set<TEntity>().Find(entity.Id);
            Context.Entry(foundEntity).CurrentValues.SetValues(entity);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed && disposing)
            {
                Context.Dispose();
            }
            Disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
