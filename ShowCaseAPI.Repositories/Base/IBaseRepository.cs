using ShowCaseAPI.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCaseAPI.Repositories.Base
{
    public interface IBaseRepository <T> where T: Entity
    {
        Task<int> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
    }
}
