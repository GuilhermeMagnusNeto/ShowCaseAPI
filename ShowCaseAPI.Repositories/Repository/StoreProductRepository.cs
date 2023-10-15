using Microsoft.EntityFrameworkCore;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Infra.Context.CrossCutting.Identity.Data;
using ShowCaseAPI.Repositories.Base;
using ShowCaseAPI.Repositories.Interface;

namespace ShowCaseAPI.Repositories.Repository
{
    public class StoreProductRepository : BaseRepository<Product>, IProductRepository
    {
        public StoreProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
