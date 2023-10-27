using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCaseAPI.Repositories.Interface
{
    public interface IShowcaseRepository : IBaseRepository<Showcase>
    {
        Task<IQueryable<Showcase>> GetShowcasesByStoreId(Guid storeId);
        Task<string> GenerateExclusiveCode();
        Task<bool> CodeExistsInDatabase(string code);
    }
}
