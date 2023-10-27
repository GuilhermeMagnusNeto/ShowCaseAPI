using Microsoft.EntityFrameworkCore;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Infra.Context.CrossCutting.Identity.Data;
using ShowCaseAPI.Repositories.Base;
using ShowCaseAPI.Repositories.Interface;

namespace ShowCaseAPI.Repositories.Repository
{
    public class StoreProductRepository : BaseRepository<StoreProduct>, IStoreProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public StoreProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<StoreProduct>> GetProductsByStoreId(Guid storeId)
        {
            return _dbContext.StoreProducts.AsNoTracking().Where(_ => !_.Deleted && _.StoreId == storeId);
        }
    }
}
