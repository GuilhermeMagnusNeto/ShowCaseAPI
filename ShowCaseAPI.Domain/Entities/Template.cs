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
    [Table(name: "templates")]
    public class Template : Entity
    {
        [Column("NAME")]
        public string Name { get; set; }
    }

    public static class TemplateDbBuilder
    {
        public static EntityTypeBuilder<TEntity> ConfigureUnique<TEntity>(this EntityTypeBuilder<TEntity> self, ModelBuilder modelBuilder) where TEntity : Template
        {
            self.HasKey(x => x.Id);
            self.Property(x => x.Id).IsRequired();
            self.Property(x => x.Deleted).HasDefaultValue(false);

            self.Property(x => x.Name).IsRequired();

            return self;
        }
    }
}
