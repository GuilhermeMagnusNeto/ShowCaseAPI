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
    [Table(name: "showcase_products")]
    public class ShowcaseProduct : Entity
    {
        [Column("SHOWCASE_ID")]
        public Guid ShowcaseId { get; set; }
        public virtual Showcase Showcase { get; set; }

        [Column("STORE_PRODUCT_ID")]
        public Guid StoreProductId { get; set; }
        public virtual StoreProduct StoreProduct { get; set; }
    }

    public static class ShowcaseProductDbBuilder
    {
        public static EntityTypeBuilder<TEntity> ConfigureUnique<TEntity>(this EntityTypeBuilder<TEntity> self, ModelBuilder modelBuilder) where TEntity : ShowcaseProduct
        {
            self.HasKey(x => x.Id);
            self.Property(x => x.Id).IsRequired();
            self.Property(x => x.Deleted).HasDefaultValue(false);

            self.Property(x => x.ShowcaseId).IsRequired();
            self.Property(x => x.StoreProductId).IsRequired();

            return self;
        }
    }
}
