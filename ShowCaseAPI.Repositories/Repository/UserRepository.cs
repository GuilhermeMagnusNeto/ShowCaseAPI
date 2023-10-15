using Microsoft.EntityFrameworkCore;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Infra.Context.CrossCutting.Identity.Data;
using ShowCaseAPI.Repositories.Base;
using ShowCaseAPI.Repositories.Interface;
using System.Runtime.InteropServices.ObjectiveC;
using System.Security.Cryptography;

namespace ShowCaseAPI.Repositories.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email.ToUpper().Equals(email.ToUpper()));
        }
    }
}
