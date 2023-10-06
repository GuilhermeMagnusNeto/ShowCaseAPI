using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowCaseAPI.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCaseAPI.Domain.Entities
{
    [Table(name: "showcases")]
    public class Showcase : Entity
    {
        [Column("EXCLUSIVE_URL")]
        public string ExclusiveURL { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("STORE_ID")]
        public Guid StoreId { get; set; }
        public virtual Store Store { get; set; }
    }

    public static class ShowcaseDbBuilder
    {
        public static EntityTypeBuilder<TEntity> ConfigureUnique<TEntity>(this EntityTypeBuilder<TEntity> self, ModelBuilder modelBuilder) where TEntity : Showcase
        {
            self.HasKey(x => x.Id);
            self.Property(x => x.Id).IsRequired();
            self.Property(x => x.Deleted).HasDefaultValue(false);

            self.HasIndex(x => x.ExclusiveURL).IsUnique();
            self.Property(x => x.ExclusiveURL).IsRequired();
            self.Property(x => x.StoreId).IsRequired();
            self.Property(x => x.Name).IsRequired();

            return self;
        }
    }
}
