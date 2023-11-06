using Microsoft.EntityFrameworkCore;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Infra.Context.CrossCutting.Identity.Data;
using ShowCaseAPI.Repositories.Base;
using ShowCaseAPI.Repositories.Extension;
using ShowCaseAPI.Repositories.Interface;
using System.Text;

namespace ShowCaseAPI.Repositories.Repository
{
    public class ShowcaseProductRepository : BaseRepository<ShowcaseProduct>, IShowcaseProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ShowcaseProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}