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
    [Table(name: "stores")]
    public class Store : Entity
    {
        [Column("NAME")]
        public string Name { get; set; }

        [Column("STORE_LOGO")]
        public string StoreLogo { get; set; }

        [Column("USER_ID")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }

    public static class StoreDbBuilder
    {
        public static EntityTypeBuilder<TEntity> ConfigureUnique<TEntity>(this EntityTypeBuilder<TEntity> self, ModelBuilder modelBuilder) where TEntity : Store
        {
            self.HasKey(x => x.Id);
            self.Property(x => x.Id).IsRequired();
            self.Property(x => x.Deleted).HasDefaultValue(false);

            self.Property(x => x.Name).IsRequired();
            self.Property(x => x.StoreLogo).IsRequired(false);
            self.Property(x => x.UserId).IsRequired();

            return self;
        }
    }
}
