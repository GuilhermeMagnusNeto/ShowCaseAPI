using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ShowCaseAPI.Domain.Entities;
using System.Reflection.Emit;

namespace ShowCaseAPI.Infra.Context.CrossCutting.Identity.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

       //Entities
        public DbSet<ShowCase> ShowCases { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Entities configurations
            modelBuilder.Entity<ShowCase>().ConfigureUnique(modelBuilder);


            //Crete
            base.OnModelCreating(modelBuilder);
        }
    }
}