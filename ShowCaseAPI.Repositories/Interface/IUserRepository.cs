using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCaseAPI.Repositories.Interface
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}
