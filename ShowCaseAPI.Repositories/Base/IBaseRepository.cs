using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCaseAPI.Repositories.Base
{
    public interface IBaseRepository <T> where T: class
    {//TODO: Conferir!
        int Insert(T entity);
        int Update(T entity);
        int Delete(Guid id);
        IEnumerable<T> GetAll();
        T GetById(Guid id);
    }
}
