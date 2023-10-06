using Microsoft.EntityFrameworkCore;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCaseAPI.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly DbContext _dbContext;

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Delete(Guid id)
        {
            var entity = GetById(id);
            entity.UpdatedAt = DateTime.UtcNow;
            entity.Deleted = true;

            Update(entity);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll() 
        {
            return _dbContext.Set<T>().AsNoTracking().Where(_ => !_.Deleted).ToList();
        }

        public T GetById(Guid id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public int Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
