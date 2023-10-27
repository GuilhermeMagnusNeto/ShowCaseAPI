using Microsoft.EntityFrameworkCore;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Infra.Context.CrossCutting.Identity.Data;
using ShowCaseAPI.Repositories.Base;
using ShowCaseAPI.Repositories.Extension;
using ShowCaseAPI.Repositories.Interface;
using System.Text;

namespace ShowCaseAPI.Repositories.Repository
{
    public class ShowcaseRepository : BaseRepository<Showcase>, IShowcaseRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ShowcaseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<Showcase>> GetShowcasesByStoreId(Guid storeId)
        {
            return _dbContext.Showcases.AsNoTracking().Where(_ => !_.Deleted && _.StoreId == storeId);
        }

        public async Task<string> GenerateExclusiveCode()
        {
            const int codeLenght = 8;
            string code;
            bool exists;

            do
            {
                code = ShowcaseExtension.GenerateRandomCode(codeLenght);
                exists = await CodeExistsInDatabase(code);
            } while (exists);

            return code;
        }

        public async Task<bool> CodeExistsInDatabase(string code)
        {
            return _dbContext.Showcases
                .AsNoTracking()
                .Where(_ => !_.Deleted)
                .Any(_ => _.ExclusiveCode == code);
        }
    }
}
