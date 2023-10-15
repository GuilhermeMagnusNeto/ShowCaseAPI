using Microsoft.EntityFrameworkCore;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Domain.Entities.Base;
using ShowCaseAPI.Infra.Context.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCaseAPI.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Delete(Guid id)
        {
            var entity = await GetById(id);
            entity.UpdatedAt = DateTime.UtcNow;
            entity.Deleted = true;
            await Update(entity);
            return _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAll() 
        {
            return _dbContext.Set<T>().AsNoTracking().Where(_ => !_.Deleted);
        }

        public async Task<T> GetById(Guid id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public async Task<int> Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public async Task<int> Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
