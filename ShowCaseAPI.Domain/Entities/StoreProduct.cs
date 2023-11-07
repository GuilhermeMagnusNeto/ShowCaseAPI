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
    [Table(name: "store_products")]
    public class StoreProduct : Entity
    {
        [Column("STORE_ID")]
        public Guid StoreId { get; set; }
        public virtual Store Store { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("VALUE")]
        public double? Value { get; set; }

        /// <summary> Stock Keeping Unit </summary>
        [Column("SKU")]
        public string SKU { get; set; }

        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [Column("PRODUCT_PICTURE")]
        public string ProductPicture { get; set; }
    }

    public static class StoreProductDbBuilder
    {
        public static EntityTypeBuilder<TEntity> ConfigureUnique<TEntity>(this EntityTypeBuilder<TEntity> self, ModelBuilder modelBuilder) where TEntity : StoreProduct
        {
            self.HasKey(x => x.Id);
            self.Property(x => x.Id).IsRequired();
            self.Property(x => x.Deleted).HasDefaultValue(false);

            self.Property(x => x.StoreId).IsRequired();
            self.Property(x => x.Name).IsRequired();
            self.Property(x => x.Value).IsRequired(false);
            self.Property(x => x.SKU).IsRequired(false);
            self.Property(x => x.Description).IsRequired(false);
            self.Property(x => x.ProductPicture).IsRequired(false);

            return self;
        }
    }
}
