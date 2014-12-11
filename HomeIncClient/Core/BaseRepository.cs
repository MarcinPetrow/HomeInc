using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HomeIncClient.Core.Store;

namespace HomeIncClient.Core
{
    public class BaseRepository<TContext, TEntity> : ICrud<TEntity>, IDisposable
        where TContext : DbContext, new()
        where TEntity : Entity
    {
        private readonly DbContext _context;

        protected BaseRepository()
        {
            _context = new TContext();
        }

        public int Create(TEntity entity)
        {
            var createdEntity = _context.Set<TEntity>().Add(entity);

            _context.SaveChanges();
            return createdEntity.Id;
        }

        public TEntity Read(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void Update(TEntity entity)
        {
            if (entity.Id == 0) return;
            entity.UpdatedAt = DateTime.Now;
            var currentEntity = _context.Set<TEntity>().Find(entity.Id);
            _context.Entry(currentEntity).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void UpdateOrCreate(TEntity entity)
        {
            entity.UpdatedAt = DateTime.Now;
            if (entity.Id != 0)
            {
                var currentEntity = _context.Set<TEntity>().Find(entity.Id);
                _context.Entry(currentEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                _context.Set<TEntity>().Add(entity);
            }

            _context.SaveChanges();
        }

        public List<TEntity> All()
        {
            return _context.Set<TEntity>().ToList();
        }
    }
}