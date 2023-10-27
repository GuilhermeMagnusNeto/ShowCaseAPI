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
        public DbSet<Showcase> Showcases { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<ShowcaseStyle> ShowcaseStyles { get; set; }
        public DbSet<StoreProduct> StoreProducts { get; set; }

        //TODO: RODAR MIGRATION
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Entities configurations
            modelBuilder.Entity<Showcase>().ConfigureUnique(modelBuilder);
            modelBuilder.Entity<User>().ConfigureUnique(modelBuilder);
            modelBuilder.Entity<Store>().ConfigureUnique(modelBuilder);
            modelBuilder.Entity<Template>().ConfigureUnique(modelBuilder);
            modelBuilder.Entity<ShowcaseStyle>().ConfigureUnique(modelBuilder);
            modelBuilder.Entity<StoreProduct>().ConfigureUnique(modelBuilder);


            //Crete
            base.OnModelCreating(modelBuilder);
        }
    }
}