using Microsoft.EntityFrameworkCore;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Infra.Context.CrossCutting.Identity.Data;
using ShowCaseAPI.Repositories.Base;
using ShowCaseAPI.Repositories.Interface;

namespace ShowCaseAPI.Repositories.Repository
{
    public class ShowcaseStyleRepository : BaseRepository<ShowcaseStyle>, IShowcaseStyleRepository
    {
        public ShowcaseStyleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
