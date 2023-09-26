using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCaseAPI.Domain.Entities
{
    [Table(name: "show_cases")]
    public class ShowCase : Entity
    {
        [Column("EXCLUSIVE_URL")]
        public string ExclusiveURL { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        //[Column("USER_ID")]
        //public string UserId { get; set; }
        //public virtual User User { get; set; }
    }

    public static class ShowCaseDbBuilder
    {
        public static EntityTypeBuilder<TEntity> ConfigureUnique<TEntity>(this EntityTypeBuilder<TEntity> self, ModelBuilder modelBuilder) where TEntity : ShowCase
        {
            self.HasKey(x => x.Id);
            self.Property(x => x.Id).IsRequired();
            self.Property(x => x.Deleted).HasDefaultValue(false);

            self.HasIndex(x => x.ExclusiveURL).IsUnique();
            self.Property(x => x.ExclusiveURL).IsRequired();
            self.Property(x => x.Name).IsRequired();

            return self;
        }
    }
}
